using PortfolioBackend.Models;

namespace PortfolioBackend.Services.Interfaces
{
    public interface IAdminRepository
    {
        Admin Get(string id);

        Dictionary<string, string>? Autheticate(string? email, string? password);
    }
}
