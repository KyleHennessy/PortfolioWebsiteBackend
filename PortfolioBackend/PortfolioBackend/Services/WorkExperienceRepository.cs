using MongoDB.Driver;
using PortfolioBackend.Models;
using PortfolioBackend.Models.Interfaces;
using PortfolioBackend.Services.Interfaces;

namespace PortfolioBackend.Services
{
    public class WorkExperienceRepository : IWorkExperienceRepository
    {
        private readonly IMongoCollection<WorkExperience> _workExperiences;

        public WorkExperienceRepository(IPortfolioDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _workExperiences = database.GetCollection<WorkExperience>(settings.PortfolioWorkExperiencesCollectionName);
        }
        public WorkExperience Create(WorkExperience workExperience)
        {
            _workExperiences.InsertOne(workExperience);
            return workExperience;
        }

        public void Delete(string id)
        {
            _workExperiences.DeleteOne(workExperience => workExperience.Id == id);
        }

        public List<WorkExperience> Get()
        {
            return _workExperiences.Find(workExperience => true).ToList();
        }

        public WorkExperience Get(string id)
        {
            return _workExperiences.Find(workExperience => workExperience.Id == id).FirstOrDefault();
        }

        public void Update(string id, WorkExperience workExperience)
        {
            _workExperiences.ReplaceOne(workExperience => workExperience.Id == id, workExperience);
        }
    }
}
