namespace bettingRouletteAPI.Helpers.Configuration
{
    public class RouletteDatabaseSettings : IRouletteDatabaseSettings
    {
        public string TokensCollectionName { get; set; }
        public string RoulettesCollectionName { get; set; }
        public string BetsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IRouletteDatabaseSettings
    {
        string TokensCollectionName { get; set; }
        string RoulettesCollectionName { get; set; }
        string BetsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
