using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnect.Utilities
{
    public interface IMongoRecord
    {
        [BsonId]
        // ReSharper disable InconsistentNaming
        MongoDB.Bson.ObjectId _id { get; set; }
    }
}