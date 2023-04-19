using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace seecreativa_backend.Core
{
    public abstract class EntityBase
    {
        [BsonId]
        public required ObjectId Id { get; set; }
    }
}
