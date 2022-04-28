using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using PortfolioBackend.Models;
using PortfolioBackend.Models.Interfaces;
using PortfolioBackend.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            var admin = _admin.Find(admin => admin.Email == email && admin.Password == password).FirstOrDefault();

            if (admin == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            #pragma warning disable CS8604 // Possible null reference argument.
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),

                Expires = DateTime.Now.AddMinutes(10),

                SigningCredentials = new SigningCredentials (
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };
            #pragma warning restore CS8604 // Possible null reference argument.

            var token = tokenHandler.CreateToken(tokenDescriptor);
            DateTime expires = (DateTime)tokenDescriptor.Expires;

            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("token", tokenHandler.WriteToken(token));
            #pragma warning disable CS8604 // Possible null reference argument.
            result.Add("expires", expires.ToString("yyyy-MM-ddTHH:mm:ss"));
            #pragma warning restore CS8604 // Possible null reference argument.

            return result;
        }
    }
}
