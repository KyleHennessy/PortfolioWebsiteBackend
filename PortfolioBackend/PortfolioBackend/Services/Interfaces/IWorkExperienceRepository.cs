using PortfolioBackend.Models;

namespace PortfolioBackend.Services.Interfaces
{
    public interface IWorkExperienceRepository
    {
        List<WorkExperience> Get();
        WorkExperience Get(string id);
        WorkExperience Create(WorkExperience workExperience);
        void Update(string id, WorkExperience workExperience);
        void Delete(string id);
    }
}
