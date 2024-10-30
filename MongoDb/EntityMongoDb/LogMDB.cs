using MongoDB.Bson.Serialization.Attributes;

namespace FilmManagerSqlServe_MongoDb.MongoDb.EntityMongoDb
{
    public class LogMDB
    {
        [BsonId, BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string LogId { get; set; } = null!;

        public string LogErrorMessage { get; set; } = null!;

        public string LogDateError { get; set; }
    }
}
