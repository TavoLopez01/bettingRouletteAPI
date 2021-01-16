using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bettingRouletteAPI.Entities
{
    public class Token
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string StringToken { get; set;}
    }
}
