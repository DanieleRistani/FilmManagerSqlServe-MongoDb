using FilmManagerSqlServe_MongoDb.MongoDb.Config;
using FilmManagerSqlServe_MongoDb.MongoDb.EntityMongoDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

        [HttpPost("Update/{name}")]
        public async void Update(string name,FilmMDB newFilmInfo)
        {
            var filter = Builders<FilmMDB>.Filter.Eq(f => f.FilmName.ToUpper(),name.ToUpper());

            var film = new FilmMDB
            {
                FilmName = newFilmInfo.FilmName,
                FilmUrlImg = newFilmInfo.FilmUrlImg,
                FilmDirector = newFilmInfo.FilmDirector,
                FilmRelaseDate = newFilmInfo.FilmRelaseDate,
                FilmCategory = newFilmInfo.FilmCategory,
                FilmsTagsPivots = []
            };

            foreach (var tag in newFilmInfo.FilmsTagsPivots)
            {
                film.FilmsTagsPivots.Add(tag);
            }

            await _employeeCollection.ReplaceOneAsync(filter, film);
        }

        [HttpPut("Add")]
        public async void Add(string film_name, string film_Ulr_img, string film_director, string film_relase_date, CategoryMDB category, List<TagMDB> tags)
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
            

            foreach (var tag in tags)
            {
                film.FilmsTagsPivots.Add(tag);   
            }

            await _employeeCollection.InsertOneAsync(film);
        }

        [HttpPost("Delete/{name}")]
        public async Task<DeleteResult> Delete(string name)
        {
            return await _employeeCollection.DeleteOneAsync(f => f.FilmName.ToUpper() == name.ToUpper());
        }


    }
}


