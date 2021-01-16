using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bettingRouletteAPI.Helpers.Configuration
{
    public class RouletteDatabaseSettings : IRouletteDatabaseSettings
    {
        public string TokensCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IRouletteDatabaseSettings
    {
        string TokensCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
