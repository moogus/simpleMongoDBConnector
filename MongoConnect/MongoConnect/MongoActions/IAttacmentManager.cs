using System.IO;
using MongoDB.Bson;

namespace MongoConnect.MongoActions
{
    public interface IAttacmentManager
    {
        ObjectId CreateAttachment(string collection, string fileLocation, string fileName);
        Stream GetAttchmentFromId(string collection, ObjectId id);
    }
}
