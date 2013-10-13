using System.Collections.Generic;
using MongoConnect.Utilities;
using MongoDB.Driver;

namespace MongoConnect.MongoActions.Classes
{
     class MongoDatabaseProvider : IDatabaseProvider
    {
        public IEnumerable<string> GetAllDatabaseNames(string server)
        {
            var thisServer = new MongoServer(new MongoServerSettings
            {
                Server = new MongoServerAddress(server, 27017)
            });
            return thisServer.GetDatabaseNames();
        }

        public MongoDatabase GetCurrentDatabase(string server, string database)
        {
            var thisServer = new MongoServer(new MongoServerSettings
            {
                Server = new MongoServerAddress(server, 27017)
            });
            return thisServer.GetDatabase(database);
        }
    }
}
