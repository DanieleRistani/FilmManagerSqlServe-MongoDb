using FilmManagerSqlServe_MongoDb.MongoDb.Config;
using FilmManagerSqlServe_MongoDb.MongoDb.EntityMongoDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FilmManagerSqlServe_MongoDb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilmMDBController : ControllerBase
    {
        private IMongoCollection<FilmMDB> _employeeCollection;
        public FilmMDBController(IOptions<FilmManagerDbConfig> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(options.Value.DatabaseName);
            _employeeCollection = mongoDb.GetCollection<FilmMDB>(options.Value.CollectionName);
        }

        [HttpGet("Index")]
        public async Task<List<FilmMDB>> Get()
        {
            return await _employeeCollection.Find(_ => true).ToListAsync();
        }


        [HttpGet("Details/{name}")]
        public async Task<List<FilmMDB>> Get(String name)
        {
            return await _employeeCollection.Find(f => f.FilmName.ToUpper() == name.ToUpper()).ToListAsync();
        }

        [HttpPut("Update/{name}")]
        public async void Update(string name, FilmMDB newFilmInfo)
        {

             var filter = Builders<FilmMDB>.Filter.Eq(f => f.FilmName, name);
             var filmSelected= await _employeeCollection.Find(f => f.FilmName.ToUpper()==name.ToUpper()).FirstOrDefaultAsync();
            
             var updateDefinition = Builders<FilmMDB>.Update
            .Set(f => f.FilmName, string.IsNullOrEmpty(newFilmInfo.FilmName) ? filmSelected.FilmName : newFilmInfo.FilmName)
            .Set(f => f.FilmUrlImg, string.IsNullOrEmpty(newFilmInfo.FilmUrlImg) ? filmSelected.FilmUrlImg : newFilmInfo.FilmUrlImg)
            .Set(f => f.FilmDirector, string.IsNullOrEmpty(newFilmInfo.FilmDirector) ? filmSelected.FilmDirector : newFilmInfo.FilmDirector)
            .Set(f => f.FilmRelaseDate, string.IsNullOrEmpty(newFilmInfo.FilmRelaseDate) ? filmSelected.FilmRelaseDate : newFilmInfo.FilmRelaseDate)
            .Set(f => f.FilmCategory, filmSelected.FilmCategory); 

            
            if (newFilmInfo.FilmsTagsPivots != null && newFilmInfo.FilmsTagsPivots.Count > 0)
            {
                updateDefinition = updateDefinition.Set(f => f.FilmsTagsPivots, newFilmInfo.FilmsTagsPivots);
            }

            await _employeeCollection.UpdateOneAsync(filter, updateDefinition);
        }

        [HttpPost("Add")]
        public async void AddFilm(string film_name, string film_Ulr_img, string film_director, string film_relase_date, CategoryMDB category)
        {
            var film = new FilmMDB
            {
                FilmName = film_name,
                FilmUrlImg = film_Ulr_img,
                FilmDirector = film_director,
                FilmRelaseDate = film_relase_date,
                FilmCategory = category,
                FilmsTagsPivots = []
            };


            //foreach (var tag in filmTags)
            //{
            //    film.FilmsTagsPivots.Add(tag);
            //}

            await _employeeCollection.InsertOneAsync(film);
        }

        [HttpDelete("Delete/{name}")]
        public async Task<DeleteResult> Delete(string name)
        {
            return await _employeeCollection.DeleteOneAsync(f => f.FilmName.ToUpper() == name.ToUpper());
        }


    }
}


