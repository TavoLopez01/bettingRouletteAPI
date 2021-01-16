using bettingRouletteAPI.Entities;
using bettingRouletteAPI.Helpers.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public void DeleteToken(Token token)
        {
            _tokensCollection.DeleteOne(token => token.Id == token.Id);
        }

        public void DeleteTokenById(string id)
        {
            _tokensCollection.DeleteOne(token => token.Id == id);
        }

    }
}
