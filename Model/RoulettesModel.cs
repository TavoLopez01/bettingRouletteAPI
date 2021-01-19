using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers;
using bettingRouletteAPI.Helpers.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
namespace bettingRouletteAPI.Model
{
    public class RoulettesModel
    {
        private readonly IMongoCollection<Roulette> _roulettesCollection;
        private readonly GlobalFunctions _globalFunctions;
        public RoulettesModel(IRouletteDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);
            _roulettesCollection = database.GetCollection<Roulette>(settings.RoulettesCollectionName);
            _globalFunctions = new GlobalFunctions();
    }
        public List<Roulette> GetRoulette()
        {
            _globalFunctions.Logger(this, "Get List of Roulettes");

            return _roulettesCollection.Find(roulette => true).ToList();
        }
        public Roulette GetRouletteById(string id)
        {
            _globalFunctions.Logger(this, string.Format("Searched next Object: {0}", id));

            return _roulettesCollection.Find<Roulette>(roulette => roulette.Id == id).FirstOrDefault();
        }
        public Roulette CreateRoulette(Roulette roulette)
        {
            _roulettesCollection.InsertOne(roulette);
            _globalFunctions.Logger(this, string.Format("Created new Object: {0}", JsonConvert.SerializeObject(roulette)));

            return roulette;
        }
        public void UpdateRoulette(string id, Roulette roulette)
        {
            _roulettesCollection.ReplaceOne(roulette => roulette.Id == id, roulette);
            _globalFunctions.Logger(this, string.Format("Updated the next Object: {0}", JsonConvert.SerializeObject(roulette)));
        }
    }
}
