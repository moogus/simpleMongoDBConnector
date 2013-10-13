namespace MongoConnect.Utilities.Classes
{
    class MongoConfig : IMongoConfig
    {
         public MongoConfig(string server, string database)
         {
             Server = server;
             Database = database;
         }

         public string Server { get; set; }
        public string Database { get; set; }
    }
}
