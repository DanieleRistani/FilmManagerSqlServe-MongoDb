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
    public class LogMDBController : ControllerBase
    {

        private IMongoCollection<LogMDB> _employeeCollection;
        public LogMDBController(IOptions<LogManagerDbConfig> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(options.Value.DatabaseName);
            _employeeCollection = mongoDb.GetCollection<LogMDB>(options.Value.CollectionName);
        }

        [HttpGet("Index")]
        public async Task<List<LogMDB>> Get()
        {
            return await _employeeCollection.Find(_ => true).ToListAsync();
        }


        [HttpGet("Details/{date}")]
        public async Task<List<LogMDB>> Get(String date)
        {
            return await _employeeCollection.Find(l => l.LogDateError.ToUpper() == date.ToUpper()).ToListAsync();
        }

        [HttpPut("Update/{name}")]
        public async void Update(string name, LogMDB newLog)
        {
            var filter = Builders<LogMDB>.Filter.Eq(f => f.LogErrorMessage,name);
            
            var logUpdate = Builders<LogMDB>.Update.Set(f => f.LogErrorMessage, newLog.LogErrorMessage).Set(f => f.LogDateError, newLog.LogDateError);  

            await _employeeCollection.UpdateOneAsync(filter,logUpdate);
        }

        [HttpPost("Add")]
        public async void AddLog(string errorMessage, string dateError)
        {
            var log = new LogMDB
            {
                LogErrorMessage = errorMessage,
                LogDateError = dateError
            };

            await _employeeCollection.InsertOneAsync(log);
        }

        [HttpDelete("Delete/{date}")]
        public async Task<DeleteResult> Delete(string date)
        {
            return await _employeeCollection.DeleteManyAsync(l => l.LogDateError.ToUpper() == date.ToUpper());
        }



    }
}
