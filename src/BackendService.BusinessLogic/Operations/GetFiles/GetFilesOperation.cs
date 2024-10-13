using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Mappers;
using BackendService.BusinessLogic.Operations.GetFiles.Models;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfos;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFiles;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.PathsPreparation;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.GetFiles;

public sealed class GetFilesOperation : IGetFilesOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IGetFileInfosTask _getFileInfosTask;
    private readonly IGetFilesTask _getFilesTask;
    private readonly IPathsPreparationTask _pathsPreparationTask;
    private readonly ISendNotificationCommandTask _sendNotificationCommandTask;
    private readonly ILogger<GetFilesOperation> _logger;

    public GetFilesOperation(
        IAuthorizationTask authorizationTask,
        IGetFileInfosTask getFileInfosTask,
        IGetFilesTask getFilesTask,
        IPathsPreparationTask pathsPreparationTask,
        ISendNotificationCommandTask sendNotificationCommandTask,
        ILogger<GetFilesOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _getFileInfosTask = getFileInfosTask;
        _getFilesTask = getFilesTask;
        _pathsPreparationTask = pathsPreparationTask;
        _sendNotificationCommandTask = sendNotificationCommandTask;
        _logger = logger;
    }

    public async Task<byte[]> GetFilesAsync(GetFilesOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileGet).ConfigureAwait(false);

        var getFileInfosTaskResponse = await _getFileInfosTask.GetAsync(request.FileCodes).ConfigureAwait(false);

        var pathsPreparationTaskResponse = await _pathsPreparationTask.PreparePathsAsync(getFileInfosTaskResponse.ToPathsPreparationTaskRequest());
        var byteArray = await _getFilesTask.GetAsync(pathsPreparationTaskResponse.ToGetFilesTaskRequest()).ConfigureAwait(false);

        await _sendNotificationCommandTask.SendAsync(new SendNotificationCommandTaskRequest(UpdateFileType.GetFiles, getFileInfosTaskResponse.FileInfos.Select(t => t.Name).ToArray())).ConfigureAwait(false);

        _logger.LogInformation($"Files successfully received");

        return byteArray;
    }
}