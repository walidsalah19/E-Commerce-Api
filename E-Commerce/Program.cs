
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce
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
            //adding dbContext 
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connictionFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

                var connectionValue = connictionFile.GetSection("constr").Value;

                options.UseSqlServer(connectionValue)
                    .LogTo(Console.WriteLine, LogLevel.Information);
            });
            //cors policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("default");
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
