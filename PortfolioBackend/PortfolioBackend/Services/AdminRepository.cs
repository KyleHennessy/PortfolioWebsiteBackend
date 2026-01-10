using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using PortfolioBackend.Models;
using PortfolioBackend.Models.Interfaces;
using PortfolioBackend.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PortfolioBackend.Services
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IMongoCollection<Admin> _admin;

        private readonly string key;

        public AdminRepository(IPortfolioDatabaseSettings settings, IMongoClient mongoClient, IConfiguration configuration)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _admin = database.GetCollection<Admin>(settings.PortfolioAdminCollectionName);
            this.key = configuration.GetValue<string>("JWT:Secret").ToString();
        }

        public Admin Get(string id)
        {
            return _admin.Find(admin => admin.Id == id).FirstOrDefault();
        }

        public Dictionary<string, string>? Autheticate(string? email, string? password)
        {
            if (email == null || password == null)
            {
                return null;
            }
            var retrievedSalt = _admin.Find(admin => admin.Email == email).FirstOrDefault()?.Salt;
            if (retrievedSalt == null)
            {
                return null;
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: password,
               salt: Encoding.ASCII.GetBytes(retrievedSalt),
               prf: KeyDerivationPrf.HMACSHA256,
               iterationCount: 100000,
               numBytesRequested: 256 / 8));

            var admin = _admin.Find(admin => admin.Email == email && admin.Password == hashed).FirstOrDefault();

            if (admin == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),

                Expires = DateTime.UtcNow.AddMinutes(60),

                SigningCredentials = new SigningCredentials (
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            DateTime expires = (DateTime)tokenDescriptor.Expires;

            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("token", tokenHandler.WriteToken(token));
            result.Add("expires", expires.ToString("yyyy-MM-ddZHH:mm:ss"));

            return result;
        }
    }
}
