using PortfolioBackend.Models;

namespace PortfolioBackend.Services.Interfaces
{
    public interface IAdminRepository
    {
        Admin Get(string id);
    }
}
