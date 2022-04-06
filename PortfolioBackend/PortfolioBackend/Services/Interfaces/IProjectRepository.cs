using PortfolioBackend.Models;

namespace PortfolioBackend.Services.Interfaces
{
    public interface IProjectRepository
    {
        List<Project> Get();
        Project Get(string id);
        Project Create(Project project);
        void Update(string id, Project project);
        void Delete(string id);
    }
}
