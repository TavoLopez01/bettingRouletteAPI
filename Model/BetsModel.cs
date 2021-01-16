using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers;
using bettingRouletteAPI.Helpers.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bettingRouletteAPI.Model
{
    public class BetsModel
    {
        private readonly IMongoCollection<Bets> _betsCollection;
        
        public BetsModel(IRouletteDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);

            _betsCollection = database.GetCollection<Bets>(settings.BetsCollectionName);
        }

        public List<Bets> Get()
        {
            return _betsCollection.Find(bet => true).ToList();
        }

        public Bets GetById(string id)
        {
            return _betsCollection.Find<Bets>(bet => bet.Id == id).FirstOrDefault();
        }

        public Bets Create(Bets bet)
        {
            _betsCollection.InsertOne(bet);
            return bet;
        }

        public void Update(string id, Bets bet)
        {
            _betsCollection.ReplaceOne(bet => bet.Id == id, bet);
        }

        public void Delete(Bets bet)
        {
            _betsCollection.DeleteOne(bet => bet.Id == bet.Id);
        }

        public void DeleteById(string id)
        {
            _betsCollection.DeleteOne(bet => bet.Id == id);
        }

    }
}
