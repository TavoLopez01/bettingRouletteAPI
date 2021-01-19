using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers;
using bettingRouletteAPI.Helpers.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Linq;
namespace bettingRouletteAPI.Model
{
    public class TokensModel
    {
        private readonly IMongoCollection<Token> _tokensCollection;
        private readonly GlobalFunctions _globalFunctions;
        public TokensModel(IRouletteDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);
            _tokensCollection = database.GetCollection<Token>(settings.TokensCollectionName);
            _globalFunctions = new GlobalFunctions();
        }
        public Token GetTokenByStringToken(string value)
        {
            _globalFunctions.Logger(this, string.Format("Searched next Object: {0}", value));

            return _tokensCollection.Find<Token>(token => token.StringToken == value).FirstOrDefault();
        }
        public Token CreateToken(Token token)
        {
            _tokensCollection.InsertOne(token);
            _globalFunctions.Logger(this, string.Format("Created new Object: {0}", JsonConvert.SerializeObject(token)));

            return token;
        }
    }
}
