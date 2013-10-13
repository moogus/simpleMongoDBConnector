using System.Collections.Generic;
using MongoConnect.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace MongoConnect.MongoActions.Classes
{
    class MongoCollectionProvider : ICollectionProvider
    {
        private readonly MongoDatabase _selectedDatabase;
        private readonly IDatabaseProvider _mongoStore;

        public MongoCollectionProvider(IDatabaseProvider mongoStore, IMongoConfig config)
        {
            _mongoStore = mongoStore;
            _selectedDatabase = _mongoStore.GetCurrentDatabase(config.Server, config.Database);
        }

        public IEnumerable<string> GetAllCollectionNames()
        {
            return _selectedDatabase.GetCollectionNames();
        }

        public MongoCollection<BsonDocument> GetCollection(string collection)
        {
            return _selectedDatabase.GetCollection(collection);
        }

        public void CreateCollection(string collectionName)
        {
            if (!_selectedDatabase.CollectionExists(collectionName))
                _selectedDatabase.CreateCollection(collectionName);
        }

        public MongoGridFS GetAttachmentStore()
        {
            return _selectedDatabase.GridFS;
        }

    }
}