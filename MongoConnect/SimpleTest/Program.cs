using System;
using MongoConnect.DependancyResolution;
using MongoConnect.Utilities;
using MongoDB.Bson;

namespace SimpleTest
{
    class Program
    {
        private static DatabaseObjectProvider _databaseObjectProvider;
        private const string ServerAddress = "192.168.56.101";//replace this with the ip of your mongo server
        private const string CollectionToUse = "test";
        static void Main(string[] args)
        {
            _databaseObjectProvider = new DatabaseObjectProvider(ServerAddress, CollectionToUse);
            var recordManager = _databaseObjectProvider.GetRecordManager();
            var objectId = recordManager.InsertRecord(CollectionToUse, new TestObject { Message = "hello world" });
            var testObj = recordManager.GetObjectById<TestObject>(CollectionToUse, objectId);
            Console.WriteLine(testObj.Message);
            Console.ReadKey();
        }

        private class TestObject : IMongoRecord
        {
            public ObjectId _id { get; set; }
            public string Message { get; set; }
        }
    }


}
