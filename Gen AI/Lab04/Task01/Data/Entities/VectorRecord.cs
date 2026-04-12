using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Task01.Data.Entities;

[BsonIgnoreExtraElements]
public sealed class VectorRecord
{
    [BsonRepresentation(BsonType.String)]
    public Guid ChunkId { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Guid DocumentId { get; set; }

    public float[] Vector { get; set; } = [];
}
