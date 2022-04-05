namespace PortfolioBackend.Models
{
    public interface IPortfolioDatabaseSettings
    {
        public string PortfolioProjectsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
