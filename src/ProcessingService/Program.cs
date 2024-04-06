using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProcessingService.Configuration;
using Serilog;

namespace ProcessingService;

public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)
            .UseSerilog()
            .UseNServiceBus(b => NsbConfiguration.GetEndpointConfiguration(b.Configuration))
            .ConfigureServices((hostContext, services) => { services.ConfigureService(hostContext); })
            .ConfigureAppConfiguration(x =>
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                x.Sources.Clear();
                x.AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true)
                    .AddEnvironmentVariables();
            });
    }
}