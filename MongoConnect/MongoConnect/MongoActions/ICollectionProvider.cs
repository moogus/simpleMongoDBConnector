using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace MongoConnect.MongoActions
{
    public interface ICollectionProvider
    {
        IEnumerable<string> GetAllCollectionNames();
        MongoCollection<BsonDocument> GetCollection(string collection);
        MongoGridFS GetAttachmentStore();
        void CreateCollection(string collectionName);
    }
}