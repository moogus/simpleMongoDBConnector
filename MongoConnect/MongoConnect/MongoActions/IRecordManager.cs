using System.Collections.Generic;
using MongoConnect.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoConnect.MongoActions
{
    public interface IRecordManager
    {
        IEnumerable<string> GetAvailableCollections();
        MongoCursor<T> GetAll<T>(string collection) where T : IMongoRecord;
        MongoCursor<T> GetAll<T>(string collection, int pageStart, int pageSize, string sortProperty) where T : IMongoRecord;
        MongoCursor<T> GetAllObjectsByProperty<T>(string collection, string queryProperty, BsonValue queryValue) where T : IMongoRecord;
        T GetObjectById<T>(string collection, ObjectId id) where T : IMongoRecord;
        ObjectId InsertRecord<T>(string collection, T what) where T : IMongoRecord;
        IEnumerable<ObjectId> InsertListOfRecords<T>(string collection, List<T> what) where T : class, IMongoRecord;
        void UpdateRecord<T>(string collection, T what) where T : IMongoRecord;
        void DeleteRecord<T>(string collection, T what) where T : IMongoRecord;
    }
}