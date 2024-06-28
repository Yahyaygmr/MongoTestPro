using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace MongoTestPro.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string? ImageStorageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string CategoryId { get; set; }

    }
}