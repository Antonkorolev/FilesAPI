using Microsoft.Extensions.Configuration;

namespace IntegrationTests;

public class IntegrationTestsBase
{
    protected HttpClient GetClient()
    {
        var configuration = GetBuilder();

        var url = configuration.GetSection("Services:FilesApiBackend").Value ?? throw new Exception("FilesApiBackend url not found in config");

        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri(url)
        };
        
        return httpClient;
    }

    private IConfigurationRoot GetBuilder()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false);

        return builder.Build();
    }
}