using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bettingRouletteAPI.Entities
{
    public class Roulette
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Status { get; set; }
    }
}
