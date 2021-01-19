using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace bettingRouletteAPI.Entities
{
    public class Roulette
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Status { get; set; }
        [BsonDateTimeOptions]
        public DateTime? CreatedAt { get; set; }
        [BsonDateTimeOptions]
        public DateTime? OpenedDate { get; set; }
        [BsonDateTimeOptions]
        public DateTime? CloseDate { get; set; }
    }
}
