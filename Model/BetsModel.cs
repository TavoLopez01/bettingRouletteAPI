using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers;
using bettingRouletteAPI.Helpers.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
namespace bettingRouletteAPI.Model
{
    public class BetsModel
    {
        private readonly IMongoCollection<Bet> _betsCollection; 
        private readonly GlobalFunctions _globalFunctions;
        public BetsModel(IRouletteDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);
            _betsCollection = database.GetCollection<Bet>(settings.BetsCollectionName);
            _globalFunctions = new GlobalFunctions();
        }
        public List<Bet> GetBet()
        {
            _globalFunctions.Logger(this, "");

            return _betsCollection.Find(bet => true).ToList();
        }
        public List<Bet> GetListBetsByIdRoulette(Roulette roulette)
        {
            var minDate = roulette.OpenedDate;
            var maxDate = roulette.CloseDate;
            var betsList = _betsCollection.Find(bet => bet.CreatedAt >= minDate & bet.CreatedAt <= maxDate).ToList();
            _globalFunctions.Logger(this, string.Format("Get the next List of bets: {0}", JsonConvert.SerializeObject(betsList)));

            return betsList;
        }
        public Bet GetBetById(string id)
        {
            return _betsCollection.Find<Bet>(bet => bet.Id == id).FirstOrDefault();
        }
        public Bet CreateBet(Bet bet)
        {
            _betsCollection.InsertOne(bet);
            _globalFunctions.Logger(this, string.Format("Created new Object: {0}", JsonConvert.SerializeObject(bet)));

            return bet;
        }
    }
}
