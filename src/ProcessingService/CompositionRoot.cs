using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent;
using ProcessingService.BusinessLogic.Operations.UploadFiles;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.DeleteFilesFromTemporaryStorage;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.ReadFilesFromTemporaryStorage;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.SaveFileInfo;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.WriteFileToPersistenceStorage;
using ProcessingService.BusinessLogic.Tasks.EnsurePathExists;
using ProcessingService.BusinessLogic.Tasks.PathBuilder;

namespace ProcessingService;

public static class CompositionRoot
{
    public static void ConfigureService(this IServiceCollection services, HostBuilderContext context)
    {
        AddFileDbContext(services, "FileDb", context.Configuration);
        AddApplyFileUpdateOperation(services, context.Configuration);
        AddUploadFilesOperation(services, context.Configuration);
        AddCommonTask(services, context.Configuration);
    }

    private static void AddApplyFileUpdateOperation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IPublishNotificationEventOperation, PublishNotificationEventOperation>();
    }

    private static void AddUploadFilesOperation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUploadFilesOperation, UploadFilesOperation>();
        services.AddTransient<IReadFilesFromTemporaryStorageTask, ReadFilesFromTemporaryStorageTask>();
        services.AddTransient<IWriteFileToPersistenceStorage, WriteFileToPersistenceStorage>();
        services.AddTransient<IDeleteFilesFromTemporaryStorageTask, DeleteFilesFromTemporaryStorageTask>();
        services.AddTransient<ISaveFileInfoTask, SaveFileInfoTask>();
    }

    private static void AddCommonTask(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPathBuilderTask>(x => new PathBuilderTask(configuration.GetRequiredSection("Storage").Value ?? throw new Exception("Storage value is null")));
        services.AddTransient<IEnsurePathExistsTask, EnsurePathExistsTask>();
    }

    private static IServiceCollection AddFileDbContext(this IServiceCollection services, string name, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(name);

        services.AddDbContext<FileDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
                options.UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>());
            })
            .AddScoped<IFileDbContext>(x => x.GetRequiredService<FileDbContext>());

        return services;
    }
}