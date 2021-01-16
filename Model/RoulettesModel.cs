using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bettingRouletteAPI.Model
{
    public class RoulettesModel
    {
        private readonly IMongoCollection<Roulettes> _roulettesCollection;

        public RoulettesModel(IRouletteDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);

            _roulettesCollection = database.GetCollection<Roulettes>(settings.RoulettesCollectionName);
        }

        public List<Roulettes> Get()
        {
            return _roulettesCollection.Find(roulette => true).ToList();
        }

        public Roulettes GetById(string id)
        {
            return _roulettesCollection.Find<Roulettes>(roulette => roulette.Id == id).FirstOrDefault();
        }

        public Roulettes Create(Roulettes roulette)
        {
            _roulettesCollection.InsertOne(roulette);
            return roulette;
        }

        public void Update(string id, Roulettes roulette)
        {
            _roulettesCollection.ReplaceOne(roulette => roulette.Id == id, roulette);
        }

        public void Delete(Roulettes roulette)
        {
            _roulettesCollection.DeleteOne(roulette => roulette.Id == roulette.Id);
        }

        public void DeleteById(string id)
        {
            _roulettesCollection.DeleteOne(roulette => roulette.Id == id);
        }
    }
}
