using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PortfolioBackend.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name")]
        [Required(ErrorMessage ="Please write your name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage ="Must be at least 3 characters and less than 50 characters long")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("email")]
        [Required(ErrorMessage = "Please write your email")]
        [StringLength(320, ErrorMessage ="Email address is too large")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("messageText")]
        [Required(ErrorMessage = "Please write your message")]
        [StringLength(500, MinimumLength =5, ErrorMessage="Must be at least 5 characters and less than 500 characters long")]
        public string MessageText { get; set; } = string.Empty;
    }
}
