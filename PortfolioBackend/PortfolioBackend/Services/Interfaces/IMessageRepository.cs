using PortfolioBackend.Models;

namespace PortfolioBackend.Services.Interfaces
{
    public interface IMessageRepository
    {
        List<Message> Get();
        Message Get(string id);
        Message Create(Message message);
        void Delete(string id);
    }
}
