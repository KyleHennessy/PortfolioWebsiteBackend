using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PortfolioBackend.Models
{
    public class WorkExperience
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("title")]
        [Required]
        public string Title { get; set; } = string.Empty;

        [BsonElement("description")]
        [Required]
        public string Description { get; set; } = string.Empty;

        [BsonElement("dateStarted")]
        [Required]
        public DateTime DateStarted { get; set; }

        [BsonElement("dateEnded")]
        public DateTime? DateEnded { get; set; }

        [Required]
        public List<Skill> SkillsUsed { get; set; } = new List<Skill>();
    }
}
