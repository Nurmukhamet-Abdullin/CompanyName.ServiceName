using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace CompanyName.ServiceName.WebApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((_, configuration) =>
                    configuration.Enrich.FromLogContext()
                        .Enrich.FromLogContext()
                        .MinimumLevel.Debug()
                        .WriteTo.Console());
    }
}
