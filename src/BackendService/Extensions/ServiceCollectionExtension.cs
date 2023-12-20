using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFileDbContext(this IServiceCollection services, string name, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(name);

        services.AddDbContext<FileDbContext>(options => options.UseSqlServer(connectionString)).
            AddScoped<IFileDbContext>(x => x.GetRequiredService<IFileDbContext>());

        return services;
    }
}