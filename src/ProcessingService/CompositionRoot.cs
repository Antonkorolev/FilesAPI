using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent;

namespace ProcessingService;

public static class CompositionRoot
{
    public static void ConfigureService(this IServiceCollection services, HostBuilderContext context)
    {
        AddApplyFileUpdateOperation(services, context.Configuration);
        //AddFileUpdateHostedService(services, context.Configuration);
    }

    private static void AddApplyFileUpdateOperation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IPublishFileUpdateEventOperation, PublishFileUpdateEventOperation>();
    }

    private static void AddFileUpdateHostedService(this IServiceCollection services, IConfiguration configuration)
    {
        
    }    
}