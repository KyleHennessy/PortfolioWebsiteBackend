using MongoDB.Driver;
using PortfolioBackend.Models;
using PortfolioBackend.Models.Interfaces;
using PortfolioBackend.Services.Interfaces;

namespace PortfolioBackend.Services
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IMongoCollection<Admin> _admin;

        public AdminRepository(IPortfolioDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _admin = database.GetCollection<Admin>(settings.PortfolioAdminCollectionName);
        }

        public Admin Get(string id)
        {
            return _admin.Find(admin => admin.Id == id).FirstOrDefault();
        }
    }
}
