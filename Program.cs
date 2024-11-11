
using FilmManagerSqlServe_MongoDb.MongoDb.Config;
using FilmManagerSqlServe_MongoDb.SqlServe.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace FilmManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(js => { js.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //SQL-SERVE
            builder.Services.AddDbContext<FilmManagerContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("FilmManager")));
            //MONGO-DB
            builder.Services.Configure<FilmManagerDbConfig>(builder.Configuration.GetSection("FilmMongoDb"));
            builder.Services.AddSingleton<FilmManagerDbConfig>();
            builder.Services.Configure<LogManagerDbConfig>(builder.Configuration.GetSection("LogMongoDb"));
            builder.Services.AddSingleton<LogManagerDbConfig>();
            
            
            //cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500").AllowAnyMethod().AllowAnyHeader();
                });
            });
     

            var app = builder.Build();

            //cors
            app.UseCors("AllowSpecificOrigin");

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
