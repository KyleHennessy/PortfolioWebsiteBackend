using PortfolioBackend.Models;
using MongoDB.Driver;

namespace PortfolioBackend.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMongoCollection<Project> _projects;

        public ProjectService(IPortfolioDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _projects = database.GetCollection<Project>(settings.PortfolioProjectsCollectionName);
        }
        public Project Create(Project project)
        {
            _projects.InsertOne(project);
            return project;
        }

        public void Delete(string id)
        {
            _projects.DeleteOne(project => project.Id == id);
        }

        public List<Project> Get()
        {
            return _projects.Find(project => true).ToList();
        }

        public Project Get(string id)
        {
            return _projects.Find(project => project.Id == id).FirstOrDefault();
        }

        public void Update(string id, Project project)
        {
            _projects.ReplaceOne(project => project.Id == id, project);
        }
    }
}
