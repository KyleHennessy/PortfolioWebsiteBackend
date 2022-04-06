using PortfolioBackend.Models;

namespace PortfolioBackend.Services.Interfaces
{
    public interface ISkillRepository
    {
        List<Skill> Get();
        Skill Get(string id);
        Skill Create(Skill skill);
        void Update(string id, Skill skill);
        void Delete(string id);
    }
}
