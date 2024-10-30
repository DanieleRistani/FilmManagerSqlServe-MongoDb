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


    }
}
