
using MongoDB.Bson.Serialization.Attributes;

namespace FilmManagerSqlServe_MongoDb.MongoDb.EntityMongoDb
{
    public class FilmMDB
    {
        [BsonId, BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string FilmId { get; set; }
        
        public string FilmName { get; set; } = null!;

        public string FilmUrlImg { get; set; } = null!;

        public string FilmDirector { get; set; } = null!;

        public string FilmRelaseDate { get; set; }

        public  CategoryMDB FilmCategory { get; set;}

        public List<TagMDB> FilmsTagsPivots { get; set; } 

    }
}
