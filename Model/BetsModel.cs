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
        private readonly IMongoCollection<Bet> _betsCollection;
        
        public BetsModel(IRouletteDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);

            _betsCollection = database.GetCollection<Bet>(settings.BetsCollectionName);
        }

        public List<Bet> GetBet()
        {
            return _betsCollection.Find(bet => true).ToList();
        }

        public Bet GetBetById(string id)
        {
            return _betsCollection.Find<Bet>(bet => bet.Id == id).FirstOrDefault();
        }

        public Bet CreateBet(Bet bet)
        {
            _betsCollection.InsertOne(bet);
            return bet;
        }

        public void UpdateBet(string id, Bet bet)
        {
            _betsCollection.ReplaceOne(bet => bet.Id == id, bet);
        }

        public void DeleteBet(Bet bet)
        {
            _betsCollection.DeleteOne(bet => bet.Id == bet.Id);
        }

        public void DeleteBetById(string id)
        {
            _betsCollection.DeleteOne(bet => bet.Id == id);
        }

    }
}
