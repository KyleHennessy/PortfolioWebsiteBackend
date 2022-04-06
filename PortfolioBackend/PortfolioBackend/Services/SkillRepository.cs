using MongoDB.Driver;
using PortfolioBackend.Models;
using PortfolioBackend.Models.Interfaces;
using PortfolioBackend.Services.Interfaces;

namespace PortfolioBackend.Services
{
    public class SkillRepository : ISkillRepository
    {
        private readonly IMongoCollection<Skill> _skills;

        public SkillRepository(IPortfolioDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _skills = database.GetCollection<Skill>(settings.PortfolioSkillsCollectionName);
        }
        public Skill Create(Skill skill)
        {
            _skills.InsertOne(skill);
            return skill;
        }

        public void Delete(string id)
        {
            _skills.DeleteOne(skill => skill.Id == id);
        }

        public List<Skill> Get()
        {
            return _skills.Find(skill => true).ToList();
        }

        public Skill Get(string id)
        {
            return _skills.Find(skill => skill.Id == id).FirstOrDefault();
        }

        public void Update(string id, Skill skill)
        {
            _skills.ReplaceOne(skill => skill.Id == id, skill);
        }
    }
}
