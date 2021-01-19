using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
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
        public List<Bet> GetListBetsByIdRoulette(Roulette roulette)
        {
            var minDate = roulette.OpenedDate;
            var maxDate = roulette.CloseDate;
            var betsList = _betsCollection.Find(bet => bet.CreatedAt >= minDate & bet.CreatedAt <= maxDate).ToList();

            return betsList;
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
    }
}
