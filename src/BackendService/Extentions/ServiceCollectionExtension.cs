using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Extentions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, string name, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("name");

        services.AddDbContext<FileDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}