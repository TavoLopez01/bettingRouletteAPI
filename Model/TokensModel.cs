using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers.Configuration;
using MongoDB.Driver;
using System.Linq;

namespace bettingRouletteAPI.Model
{
    public class TokensModel
    {
        private readonly IMongoCollection<Token> _tokensCollection;
        public TokensModel(IRouletteDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);
            _tokensCollection = database.GetCollection<Token>(settings.TokensCollectionName);
        }

        public Token GetTokenByStringToken(string value)
        {
            return _tokensCollection.Find<Token>(token => token.StringToken == value).FirstOrDefault();
        }

        public Token CreateToken(Token token)
        {
            _tokensCollection.InsertOne(token);

            return token;
        }
    }
}
