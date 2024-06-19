using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoTestPro.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryId { get; set; }

    }
}