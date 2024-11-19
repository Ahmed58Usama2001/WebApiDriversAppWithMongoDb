using MongoDB.Bson.Serialization.Attributes;

namespace WebApiDriversAppWithMongoDb.Models;

public class Driver
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    [BsonElement("_id")]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string DriverName { get; set; } = null!;

    [BsonElement("number")]
    public int Number { get; set; }

    [BsonElement("team")]
    public string Team { get; set; } = null!;
}
