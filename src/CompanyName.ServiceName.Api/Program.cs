using CompanyName.ServiceName.DataAccess;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Threading.Tasks;

namespace CompanyName.ServiceName.Api
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            //host.Services
            //    .GetRequiredService<IMigrationRunner>()
            //    .MigrateUp();

            await host.Services.GetRequiredService<DataContext>()
                .Database.EnsureCreatedAsync();

            await host.RunAsync();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((context, configuration) =>
                    configuration.Enrich.FromLogContext()
                        .ReadFrom.Configuration(context.Configuration));
    }
}
