using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoTestPro.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }

        public string OrderAddress { get; set; }
        public string OrderDescription { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }

        public List<OrderRow> OrderRows { get; set; }
    }
}