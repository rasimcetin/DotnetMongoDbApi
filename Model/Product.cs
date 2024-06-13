
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetMongoDbApi.Model
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
    }
}