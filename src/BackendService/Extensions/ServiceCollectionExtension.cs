using BackendService.BusinessLogic.Operations.DeleteFile;
using BackendService.BusinessLogic.Operations.DeleteFile.Tasks.DeleteFileInfo;
using BackendService.BusinessLogic.Operations.DeleteFiles;
using BackendService.BusinessLogic.Operations.GetFile;
using BackendService.BusinessLogic.Operations.GetFile.Tasks.GetFile;
using BackendService.BusinessLogic.Operations.GetFiles;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFiles;
using BackendService.BusinessLogic.Operations.UpdateFile;
using BackendService.BusinessLogic.Operations.UpdateFile.Tasks.UpdateFile;
using BackendService.BusinessLogic.Operations.UpdateFile.Tasks.UpdateFileInfo;
using BackendService.BusinessLogic.Operations.UpdateFiles;
using BackendService.BusinessLogic.Operations.UpdateFiles.Tasks;
using BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.SendUpdateFilesCommand;
using BackendService.BusinessLogic.Operations.UploadFile;
using BackendService.BusinessLogic.Operations.UploadFile.Tasks.SaveFileInfo;
using BackendService.BusinessLogic.Operations.UploadFiles;
using BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.DeleteFile;
using BackendService.BusinessLogic.Tasks.EnsurePathExists;
using BackendService.BusinessLogic.Tasks.GetFileInfo;
using BackendService.BusinessLogic.Tasks.GetFileInfos;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.PathsPreparation;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using BackendService.BusinessLogic.Tasks.WriteFile;
using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFileDbContext(this IServiceCollection services, string name, IConfiguration configuration)
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

    public static IServiceCollection AddUploadFileOperation(this IServiceCollection services)
    {
        services.AddTransient<IUploadFileOperation, UploadFileOperation>();
        services.AddTransient<ISaveFileInfoTask, SaveFileInfoTask>();

        return services;
    }

    public static IServiceCollection AddUploadFilesOperation(this IServiceCollection services)
    {
        services.AddTransient<IUploadFilesOperation, UploadFilesOperation>();
        services.AddTransient<ISendUploadFilesCommandTask, SendUploadFilesCommandTask>();

        return services;
    }

    public static IServiceCollection AddUpdateFileOperation(this IServiceCollection services)
    {
        services.AddTransient<IUpdateFileOperation, UpdateFileOperation>();
        services.AddTransient<IUpdateFileTask, UpdateFileTask>();
        services.AddTransient<IUpdateFileInfoTask, UpdateFileInfoTask>();

        return services;
    }
    
    public static IServiceCollection AddUpdateFilesOperation(this IServiceCollection services)
    {
        services.AddTransient<IUpdateFilesOperation, UpdateFilesOperation>();
        services.AddTransient<ISendUpdateFilesCommandTask, SendUpdateFilesCommandTask>();

        return services;
    }

    public static IServiceCollection AddGetFilesOperation(this IServiceCollection services)
    {
        services.AddTransient<IGetFilesOperation, GetFilesOperation>();
        services.AddTransient<IGetFilesTask, GetFilesTask>();

        return services;
    }

    public static IServiceCollection AddGetFileOperation(this IServiceCollection services)
    {
        services.AddTransient<IGetFileOperation, GetFileOperation>();
        services.AddTransient<IGetFileTask, GetFileTask>();

        return services;
    }

    public static IServiceCollection AddDeleteFileOperation(this IServiceCollection services)
    {
        services.AddTransient<IDeleteFileOperation, DeleteFileOperation>();
        services.AddTransient<IDeleteFileInfoTask, DeleteFileInfoTask>();

        return services;
    }

    public static IServiceCollection AddDeleteFilesOperation(this IServiceCollection services)
    {
        services.AddTransient<IDeleteFilesOperation, DeleteFilesOperation>();

        return services;
    }

    public static IServiceCollection AddCommonTasks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IGetFileInfoTask, GetFileInfoTask>();
        services.AddTransient<IAuthorizationTask, AuthorizationTask>();
        services.AddTransient<IPathsPreparationTask, PathsPreparationTask>();
        services.AddTransient<IDeleteFileTask, DeleteFileTask>();
        services.AddTransient<ISendNotificationCommandTask, SendNotificationCommandTask>();
        services.AddTransient<IEnsurePathExistsTask, EnsurePathExistsTask>();
        services.AddTransient<IWriteFileTask, WriteFileTask>();
        services.AddSingleton<IPathBuilderTask>(x => new PathBuilderTask(configuration.GetRequiredSection("Storage").Value ?? throw new Exception("Storage value is null")));
        services.AddTransient<IGetFileInfosTask, GetFileInfosTask>();

        return services;
    }
}