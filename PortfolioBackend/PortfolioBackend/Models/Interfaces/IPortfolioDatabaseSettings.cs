namespace PortfolioBackend.Models.Interfaces
{
    public interface IPortfolioDatabaseSettings
    {
        public string PortfolioProjectsCollectionName { get; set; }
        public string PortfolioSkillsCollectionName { get; set; }
        public string PortfolioWorkExperiencesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
