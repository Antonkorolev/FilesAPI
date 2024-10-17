using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent;
using ProcessingService.BusinessLogic.Operations.UpdateFiles;
using ProcessingService.BusinessLogic.Operations.UpdateFiles.Tasks.UpdateFile;
using ProcessingService.BusinessLogic.Operations.UpdateFiles.Tasks.UpdateFileInfo;
using ProcessingService.BusinessLogic.Operations.UploadFiles;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.WriteFile;
using ProcessingService.BusinessLogic.Tasks.DeleteFile;
using ProcessingService.BusinessLogic.Tasks.EnsurePathExists;
using ProcessingService.BusinessLogic.Tasks.GetFileInfos;
using ProcessingService.BusinessLogic.Tasks.PathBuilder;
using ProcessingService.BusinessLogic.Tasks.ReadFile;
using ProcessingService.BusinessLogic.Tasks.SaveFileInfo;

namespace ProcessingService;

public static class CompositionRoot
{
    public static void ConfigureService(this IServiceCollection services, HostBuilderContext context)
    {
        AddFileDbContext(services, "FileDb", context.Configuration);
        AddApplyFileUpdateOperation(services, context.Configuration);
        AddUploadFilesOperation(services, context.Configuration);
        AddUpdateFilesOperation(services, context.Configuration);
        AddCommonTask(services, context.Configuration);
    }

    private static void AddApplyFileUpdateOperation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IPublishNotificationEventOperation, PublishNotificationEventOperation>();
    }

    private static void AddUploadFilesOperation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUploadFilesOperation, UploadFilesOperation>();
        services.AddTransient<IWriteFileTask, WriteFileTask>();
    }

    private static void AddUpdateFilesOperation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUpdateFilesOperation, UpdateFilesOperation>();
        services.AddTransient<IUpdateFileInfoTask, UpdateFileInfoTask>();
        services.AddTransient<IUpdateFileTask, UpdateFileTask>();
    }

    private static void AddCommonTask(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPathBuilderTask>(x => new PathBuilderTask(configuration.GetRequiredSection("Storage").Value ?? throw new Exception("Storage value is null")));
        services.AddTransient<IDeleteFileTask, DeleteFileTask>();
        services.AddTransient<IEnsurePathExistsTask, EnsurePathExistsTask>();
        services.AddTransient<IGetFileInfosTask, GetFileInfosTask>();
        services.AddTransient<IReadFileTask, ReadFileTask>();
        services.AddTransient<ISaveFileInfoTask, SaveFileInfoTask>();
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