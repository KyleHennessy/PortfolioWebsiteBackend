namespace PortfolioBackend.Models
{
    public class PortfolioDatabaseSettings : IPortfolioDatabaseSettings
    {
        public string PortfolioProjectsCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
