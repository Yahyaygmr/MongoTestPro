using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace MongoTestPro.Dtos.ProductDtos
{
    public class UpdateProductDto
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string? ImageStorageName{ get; set; }
        public IFormFile ImageFile { get; set; }
        public string CategoryId { get; set; }

    }
}