using BackendService.BusinessLogic.Operations.DeleteFileOperation;
using BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileInfoTask;
using BackendService.BusinessLogic.Operations.GetFileOperation;
using BackendService.BusinessLogic.Operations.GetFileOperation.Tasks.GetFileTask;
using BackendService.BusinessLogic.Operations.GetFilesOperation;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFileInfosTask;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;
using BackendService.BusinessLogic.Operations.UpdateFileOperation;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileInfoTask;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileTask;
using BackendService.BusinessLogic.Operations.UploadFileOperation;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.EnsurePathExistsTask;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.GenerateFileCodeTask;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.SaveFileInfoTask;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.WriteFileTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using BackendService.BusinessLogic.Tasks.DeleteFileTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask;
using BackendService.BusinessLogic.Tasks.PathsPreparationTask;
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
        services.AddTransient<IWriteFileTask, WriteFileTask>();
        services.AddTransient<ISaveFileInfoTask, SaveFileInfoTask>();
        services.AddTransient<IEnsurePathExistsTask, EnsurePathExistsTask>();
        services.AddTransient<IGenerateFileCodeTask, GenerateFileCodeTask>();

        return services;
    }

    public static IServiceCollection AddUpdateFileOperation(this IServiceCollection services)
    {
        services.AddTransient<IUpdateFileOperation, UpdateFileOperation>();
        services.AddTransient<IUpdateFileTask, UpdateFileTask>();
        services.AddTransient<IUpdateFileInfoTask, UpdateFileInfoTask>();

        return services;
    }

    public static IServiceCollection AddGetFilesOperation(this IServiceCollection services)
    {
        services.AddTransient<IGetFilesOperation, GetFilesOperation>();
        services.AddTransient<IGetFilesTask, GetFilesTask>();
        services.AddTransient<IGetFileInfosTask, GetFileInfosTask>();

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

    public static IServiceCollection AddCommonTasks(this IServiceCollection services)
    {
        services.AddTransient<IGetFileInfoTask, GetFileInfoTask>();
        services.AddTransient<IAuthorizationTask, AuthorizationTask>();
        services.AddTransient<IPathsPreparationTask, PathsPreparationTask>();
        services.AddTransient<IDeleteFileTask, DeleteFileTask>();

        return services;
    }
}