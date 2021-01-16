using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bettingRouletteAPI.Entities
{
    public class Bets
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Id_Roulette { get; set; }
        public string Type_Bet { get; set; }
        public string Value_Bet { get; set; }
        public string Bet_Amount { get; set; }
    }
}
