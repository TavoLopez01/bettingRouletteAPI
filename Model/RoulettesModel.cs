using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bettingRouletteAPI.Model
{
    public class RoulettesModel
    {
        private readonly IMongoCollection<Roulette> _roulettesCollection;

        public RoulettesModel(IRouletteDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);
            _roulettesCollection = database.GetCollection<Roulette>(settings.RoulettesCollectionName);
        }

        public List<Roulette> GetRoulette()
        {
            return _roulettesCollection.Find(roulette => true).ToList();
        }

        public Roulette GetRouletteById(string id)
        {
            return _roulettesCollection.Find<Roulette>(roulette => roulette.Id == id).FirstOrDefault();
        }

        public Roulette CreateRoulette(Roulette roulette)
        {
            _roulettesCollection.InsertOne(roulette);

            return roulette;
        }

        public void UpdateRoulette(string id, Roulette roulette)
        {
            _roulettesCollection.ReplaceOne(roulette => roulette.Id == id, roulette);
        }
    }
}
