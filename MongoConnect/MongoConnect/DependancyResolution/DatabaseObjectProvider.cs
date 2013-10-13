using MongoConnect.MongoActions;
using MongoConnect.MongoActions.Classes;
using MongoConnect.Utilities;
using MongoConnect.Utilities.Classes;
using StructureMap;

namespace MongoConnect.DependancyResolution
{
    public class DatabaseObjectProvider
    {
       private readonly IContainer _container;
        private static string _server;
        private static string _database;

        public DatabaseObjectProvider(string server, string database)
        {
            _server = server;
            _database = database;
            _container = ConfigureDependencies();
        }

        private static IContainer ConfigureDependencies()
        {
            return new Container(x =>
                {
                    x.For<IMongoConfig>().Singleton().Use<MongoConfig>().Ctor<string>("server").Is(_server).Ctor<string>("database").Is(_database);
                    x.For<IDatabaseProvider>().Use<MongoDatabaseProvider>();
                    x.For<ICollectionProvider>().Use<MongoCollectionProvider>();
                    x.For<IAttacmentManager>().Use<MongoAttacmentManager>();
                    x.For<IRecordManager>().Use<MongoRecordManager>();
                });
        }

        public IRecordManager GetRecordManager()
        {
            return _container.GetInstance<IRecordManager>();
        }

        public IAttacmentManager GetAttachmentManager()
        {
            return _container.GetInstance<IAttacmentManager>();
        }
    }
}
