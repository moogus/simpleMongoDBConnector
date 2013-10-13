using System.Collections.Generic;
using System.Linq;
using MongoConnect.Utilities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoConnect.MongoActions.Classes
{
     class MongoRecordManager : IRecordManager
    {
        private readonly ICollectionProvider _collectionProvider;

        public MongoRecordManager(ICollectionProvider collectionProvider)
        {
            _collectionProvider = collectionProvider;
        }

        public IEnumerable<string> GetAvailableCollections()
        {
            return _collectionProvider.GetAllCollectionNames();
        }

        public MongoCursor<T> GetAll<T>(string collection) where T : IMongoRecord
        {
            var currentCollection = _collectionProvider.GetCollection(collection);
            return currentCollection.FindAs<T>(new QueryDocument());
        }

        public MongoCursor<T> GetAll<T>(string collection, int pageStart, int pageSize, string sortProperty) where T : IMongoRecord
        {
            var currentCollection = _collectionProvider.GetCollection(collection);
            return currentCollection.FindAs<T>(new QueryDocument())
                                     .SetSortOrder(SortBy.Ascending(sortProperty))
                                     .SetSkip(pageStart)
                                     .SetLimit(pageSize);
        }

        public MongoCursor<T> GetAllObjectsByProperty<T>(string collection, string queryProperty, BsonValue queryValue) where T : IMongoRecord
        {
            var query = Query.EQ(queryProperty, queryValue);
            var currentCollection = _collectionProvider.GetCollection(collection);
            return currentCollection.FindAs<T>(query);
        }

        public T GetObjectById<T>(string collection, ObjectId id) where T : IMongoRecord
        {
            var currentCollection = _collectionProvider.GetCollection(collection);
            var bsonObject = currentCollection.FindOneById(id);
            return BsonSerializer.Deserialize<T>(bsonObject);
        }

        public ObjectId InsertRecord<T>(string collection, T what) where T : IMongoRecord
        {
            var id = ObjectId.GenerateNewId();
            what._id = id;
            var currentCollection = _collectionProvider.GetCollection(collection);
            currentCollection.Insert(what.ToBsonDocument());
            return id;
        }

        public IEnumerable<ObjectId> InsertListOfRecords<T>(string collection, List<T> what) where T : class, IMongoRecord
        {
            var rtn = new List<ObjectId>();
            foreach (var record in what)
            {
                var id = ObjectId.GenerateNewId();
                record._id = id;
                rtn.Add(id);
            }

            var bsonList = what.Select(BsonExtensionMethods.ToBsonDocument<T>);

            var currentCollection = _collectionProvider.GetCollection(collection);
            currentCollection.InsertBatch(bsonList);
            return rtn;
        }

        public void UpdateRecord<T>(string collection, T what) where T : IMongoRecord
        {
            var currentCollection = _collectionProvider.GetCollection(collection);
            currentCollection.Save(what.ToBsonDocument());
        }

        public void DeleteRecord<T>(string collection, T what) where T : IMongoRecord
        {
            var query = Query<T>.EQ(e => e._id, what._id);

            var currentCollection = _collectionProvider.GetCollection(collection);
            currentCollection.Remove(query);
        }

    }
}
