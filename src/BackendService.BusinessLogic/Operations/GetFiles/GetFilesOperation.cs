using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Mappers;
using BackendService.BusinessLogic.Operations.GetFiles.Models;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfos;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFiles;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.PathsPreparation;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.GetFiles;

public sealed class GetFilesOperation : IGetFilesOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IGetFileInfosTask _getFileInfosTask;
    private readonly IGetFilesTask _getFilesTask;
    private readonly IPathsPreparationTask _pathsPreparationTask;
    private readonly ILogger<GetFilesOperation> _logger;

    public GetFilesOperation(
        IAuthorizationTask authorizationTask,
        IGetFileInfosTask getFileInfosTask,
        IGetFilesTask getFilesTask,
        IPathsPreparationTask pathsPreparationTask,
        ILogger<GetFilesOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _getFileInfosTask = getFileInfosTask;
        _getFilesTask = getFilesTask;
        _pathsPreparationTask = pathsPreparationTask;
        _logger = logger;
    }

    public async Task<byte[]> GetFilesAsync(GetFilesOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileGet).ConfigureAwait(false);

        var getFileInfosTaskResponse = await _getFileInfosTask.GetAsync(request.FileCodes).ConfigureAwait(false);

        var pathsPreparationTaskResponse = _pathsPreparationTask.PreparePaths(getFileInfosTaskResponse.ToPathsPreparationTaskRequest());
        var byteArray = _getFilesTask.Get(pathsPreparationTaskResponse.ToGetFilesTaskRequest());

        _logger.LogInformation($"Files successfully received");

        return byteArray;
    }
}