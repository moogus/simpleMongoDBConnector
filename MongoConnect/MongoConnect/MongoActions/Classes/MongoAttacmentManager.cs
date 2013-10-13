using System.IO;
using MongoDB.Bson;

namespace MongoConnect.MongoActions.Classes
{
     class MongoAttacmentManager : IAttacmentManager
    {
        private readonly ICollectionProvider _collectionProvider;

        public MongoAttacmentManager(ICollectionProvider collectionProvider)
        {
            _collectionProvider = collectionProvider;
        }

        public ObjectId CreateAttachment(string collection, string fileLocation, string fileName)
        {
            _collectionProvider.GetCollection(collection);
            var attachmentStore = _collectionProvider.GetAttachmentStore();
            var fileStream = new FileStream(fileLocation, FileMode.Open);
            var fileInfo = attachmentStore.Upload(fileStream, fileName);
            return (ObjectId)fileInfo.Id != ObjectId.Empty ? (ObjectId)fileInfo.Id : ObjectId.Empty;
        }

        public Stream GetAttchmentFromId(string collection, ObjectId id)
        {
            _collectionProvider.GetCollection(collection);
            var attachmentStore = _collectionProvider.GetAttachmentStore();
            var file = attachmentStore.FindOneById(id);
            return file.OpenRead();
        }
    }
}