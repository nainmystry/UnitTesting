
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Infrastructure;

namespace RoomBooking.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var connString = "Datasource=:memory:";
            var conn = new SqliteConnection(connString);

            conn.Open();

            builder.Services.AddDbContext<RoomBookingAppDbContext>(opt => opt.UseSqlite(conn));
            builder.Services.AddScoped<IRoomBookingRequestProcessor, RoomBookingRequestProcessor>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title="RoomBookingApp.API", Version="v1" });
            });

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



// reference : https://github.com/trevoirwilliams/RoomBookingApp-DotNetCore-TDD/tree/master