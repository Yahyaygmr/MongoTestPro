using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoTestPro.Entities
{
    public class OrderRow
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderRowId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public int ProductCount { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }
    }
}