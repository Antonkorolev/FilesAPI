using Common;
using Microsoft.Extensions.Configuration;

namespace ProcessingService.Configuration;

public static class NsbConfiguration
{
    public static EndpointConfiguration GetEndpointConfiguration(IConfiguration configuration)
    {
        var endpointConfiguration = new EndpointConfiguration(Endpoints.ProcessingEndpoint);

        endpointConfiguration.UseSerialization<SystemJsonSerializer>();
        endpointConfiguration.EnableCallbacks();
        endpointConfiguration.MakeInstanceUniquelyAddressable(Endpoints.ProcessingEndpoint);

        var connectionOptions = configuration.GetRequiredSection("Messaging:Connection").Get<NsbConnectionOptions>()
            ?? throw new Exception("Can't read Messaging:Connection from config");
        
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        
        transport.UseConventionalRoutingTopology(QueueType.Quorum);
        transport.ConnectionString(GetConnectionString(connectionOptions.EndPoints[0]));

        return endpointConfiguration;
    }

    private static string GetConnectionString(NsbEndPoint endpoint) => $"host={endpoint.Host};port={endpoint.Port};virtualhost={endpoint.VirtualHost};username={endpoint.Login};password={endpoint.Password}";
}