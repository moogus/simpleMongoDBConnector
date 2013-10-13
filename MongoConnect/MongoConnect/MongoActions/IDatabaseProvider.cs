using System.Collections.Generic;
using MongoDB.Driver;

namespace MongoConnect.MongoActions
{
    public interface IDatabaseProvider
    {
        IEnumerable<string> GetAllDatabaseNames(string server);
        MongoDatabase GetCurrentDatabase(string server, string database);
    }
}