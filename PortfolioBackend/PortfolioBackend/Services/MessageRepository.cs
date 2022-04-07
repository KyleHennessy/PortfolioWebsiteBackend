using MongoDB.Driver;
using PortfolioBackend.Models;
using PortfolioBackend.Models.Interfaces;
using PortfolioBackend.Services.Interfaces;

namespace PortfolioBackend.Services
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoCollection<Message> _messages;

        public MessageRepository(IPortfolioDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _messages = database.GetCollection<Message>(settings.PortfolioMessagesCollectionName);
        }
        public Message Create(Message message)
        {
            _messages.InsertOne(message);
            return message;
        }

        public void Delete(string id)
        {
            _messages.DeleteOne(message => message.Id == id);
        }

        public List<Message> Get()
        {
            return _messages.Find(message => true).ToList();
        }

        public Message Get(string id)
        {
            return _messages.Find(message => message.Id == id).FirstOrDefault();
        }
    }
}
