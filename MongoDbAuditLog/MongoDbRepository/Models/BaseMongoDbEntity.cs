using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbAuditLog.MongoDbRepository.Models
{
    public abstract class BaseMongoDbEntity
    {
        public BaseMongoDbEntity()
        {
            CreatedDate = DateTime.Now;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement(Order = 0)]
        public ObjectId ID { get; } = ObjectId.GenerateNewId();

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime CreatedDate { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime? ModifiedDate { get; set; }
    }
}
