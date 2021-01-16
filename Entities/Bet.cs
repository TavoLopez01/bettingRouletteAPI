using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace bettingRouletteAPI.Entities
{
    public class Bet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Id_Roulette { get; set; }
        public string Type_Bet { get; set; }
        public string Value_Bet { get; set; }
        public int Bet_Amount { get; set; }
    }
}
