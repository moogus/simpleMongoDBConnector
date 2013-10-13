namespace MongoConnect.Utilities
{
    internal interface IMongoConfig
    {
        string Server { get; set; }
        string Database { get; set; }
    }
}