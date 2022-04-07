using PortfolioBackend.Models.Interfaces;

namespace PortfolioBackend.Models
{
    public class PortfolioDatabaseSettings : IPortfolioDatabaseSettings
    {
        public string PortfolioProjectsCollectionName { get; set; } = string.Empty;
        public string PortfolioSkillsCollectionName { get; set; } = string.Empty;
        public string PortfolioWorkExperiencesCollectionName { get; set; } = string.Empty;
        public string PortfolioMessagesCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
