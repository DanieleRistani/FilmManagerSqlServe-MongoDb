
using FilmManagerSqlServe_MongoDb.MongoDb.Config;
using FilmManagerSqlServe_MongoDb.SqlServe.Context;
using Microsoft.EntityFrameworkCore;

namespace FilmManagerSqlServe_MongoDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //MongoDb
            builder.Services.Configure<FilmManagerDbConfig>(builder.Configuration.GetSection("FilmMongoDb"));
            builder.Services.AddSingleton<FilmManagerDbConfig>();
            builder.Services.Configure<LogManagerDbConfig>(builder.Configuration.GetSection("LogMongoDb"));
            builder.Services.AddSingleton<LogManagerDbConfig>();

            //SqlServe
            builder.Services.AddDbContext<FilmManagerContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("FilmDb")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
