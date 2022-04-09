using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortfolioBackend.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("summary")]
        public string Summary { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("thumbnailUrl")]
        public string ThumbnailUrl { get; set; } = string.Empty;

        [BsonElement("previewUrl")]
        public string PreviewUrl { get; set; } = string.Empty;

        [BsonElement("demoUrl")]
        public string DemoUrl { get; set; } = string.Empty;

        public List<string> DetailImagesUrl { get; set; } = new List<string>();

        [BsonElement("sourceCodeUrl")]
        public string SourceCodeUrl { get; set; } = string.Empty;


        public List<Skill> SkillsUsed { get; set; } = new List<Skill>();

    }
}
