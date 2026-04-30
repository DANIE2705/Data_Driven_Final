using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data_Driven_Final.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // Common fields
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public decimal? Price { get; set; }

        // Document 1
        [BsonElement("category")]
        public string? Category { get; set; }

        [BsonElement("inStock")]
        public bool InStock { get; set; }

        [BsonElement("tags")]
        public List<string>? Tags { get; set; }

        [BsonElement("specifications")]
        public Specifications? Specifications { get; set; }

        // Document 2
        [BsonElement("material")]
        public string? Material { get; set; }

        [BsonElement("capacityOz")]
        public int? CapacityOz { get; set; }

        [BsonElement("reviews")]
        public List<Review>? Reviews { get; set; }

        // Document 3
        [BsonElement("categories")]
        public List<string>? Categories { get; set; }

        [BsonElement("warrantyMonths")]
        public int? WarrantyMonths { get; set; }
    }

    public class Review
    {
        [BsonElement("rating")]
        public int? Rating { get; set; }

        [BsonElement("comment")]
        public string? Comment { get; set; }
    }

    public class Specifications
    {
        [BsonElement("color")]
        public string? Color { get; set; }

        [BsonElement("batteryLife")]
        public string? BatteryLife { get; set; }

        [BsonElement("connection")]
        public string? Connection { get; set; }

        [BsonElement("processor")]
        public Processor? Processor { get; set; }

        [BsonElement("graphics")]
        public Graphics? Graphics { get; set; }

        [BsonElement("memoryGB")]
        public int? MemoryGB { get; set; }

        [BsonElement("storage")]
        public Storage? Storage { get; set; }
    }

    public class Processor
    {
        [BsonElement("brand")]
        public string? Brand { get; set; }

        [BsonElement("model")]
        public string? Model { get; set; }

        [BsonElement("cores")]
        public int? Cores { get; set; }
    }

    public class Graphics
    {
        [BsonElement("brand")]
        public string? Brand { get; set; }

        [BsonElement("model")]
        public string? Model { get; set; }
    }

    public class Storage
    {
        [BsonElement("type")]
        public string? Type { get; set; }

        [BsonElement("capacityGB")]
        public int? CapacityGB { get; set; }
    }
}
