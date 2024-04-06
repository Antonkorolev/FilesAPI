using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProcessingService;

public static class CompositionRoot
{
    public static void ConfigureService(this IServiceCollection services, HostBuilderContext context)
    {
        AddPublishFileUpdateEventOperation(services, context.Configuration);
        AddFileUpdateHostedService(services, context.Configuration);
    }

    private static void AddPublishFileUpdateEventOperation(this IServiceCollection services, IConfiguration configuration)
    {
        
    }

    private static void AddFileUpdateHostedService(this IServiceCollection services, IConfiguration configuration)
    {
        
    }    
}