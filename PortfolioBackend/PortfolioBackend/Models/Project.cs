using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PortfolioBackend.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("title")]
        [Required]
        public string Title { get; set; } = string.Empty;

        [BsonElement("summary")]
        [Required]
        public string Summary { get; set; } = string.Empty;

        [BsonElement("description")]
        [Required]
        public string Description { get; set; } = string.Empty;

        [BsonElement("thumbnailUrl")]
        [Required]
        public string ThumbnailUrl { get; set; } = string.Empty;

        [BsonElement("previewUrl")]
        [Required]
        public string PreviewUrl { get; set; } = string.Empty;

        [BsonElement("demoUrl")]
        [Required]
        public string DemoUrl { get; set; } = string.Empty;

        [Required]
        public List<string> DetailImagesUrl { get; set; } = new List<string>();

        [BsonElement("sourceCodeUrl")]
        [Required]
        public string SourceCodeUrl { get; set; } = string.Empty;

        [BsonElement("liveSiteUrl")]
        public string LiveSiteUrl { get; set; } = string.Empty;

        [Required]
        public List<Skill> SkillsUsed { get; set; } = new List<Skill>();

    }
}
