using FindUsHere.DbConnector;
using FindUsHere.General.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;


namespace FindUsHere.RestApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.RegisterServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddControllers();
            webApplicationBuilder.Services.AddSingleton(DbFactory.Create());

            return webApplicationBuilder;
        }

    }
}
