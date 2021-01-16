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

        private readonly IMongoCollection<Tokens> _tokensCollection;

        public TokensModel(IRouletteDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);

            _tokensCollection = database.GetCollection<Tokens>(settings.TokensCollectionName);
        }

        public List<Tokens> Get()
        {
            return _tokensCollection.Find(token => true).ToList();
        }

        public Tokens GetById(string id)
        {
            return _tokensCollection.Find<Tokens>(token => token.Id == id).FirstOrDefault();
        }

        public Tokens Create(Tokens token)
        {
            _tokensCollection.InsertOne(token);
            return token;
        }

        public void Update(string id, Tokens token)
        {
            _tokensCollection.ReplaceOne(token => token.Id == id, token);
        }

        public void Delete(Tokens token)
        {
            _tokensCollection.DeleteOne(token => token.Id == token.Id);
        }

        public void DeleteById(string id)
        {
            _tokensCollection.DeleteOne(token => token.Id == id);
        }

    }
}
