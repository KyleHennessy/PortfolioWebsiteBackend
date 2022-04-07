﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortfolioBackend.Models
{
    public class WorkExperience
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("dateStarted")]
        public DateTime DateStarted { get; set; }

        [BsonElement("dateEnded")]
        public DateTime? DateEnded { get; set; }

        public List<Skill> SkillUsed { get; set; } = new List<Skill>();
    }
}