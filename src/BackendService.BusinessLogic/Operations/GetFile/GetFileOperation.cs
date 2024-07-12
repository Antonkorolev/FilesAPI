using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Operations.GetFile.Models;
using BackendService.BusinessLogic.Operations.GetFile.Tasks.GetFile;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.GetFileInfo;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.GetFile;

public sealed class GetFileOperation : IGetFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IGetFileInfoTask _getFileInfoTask;
    private readonly IGetFileTask _getFileTask;
    private readonly ISendNotificationCommandTask _sendNotificationCommandTask;
    private readonly IPathBuilderTask _pathBuilderTask;
    private readonly ILogger<GetFileOperation> _logger;

    public GetFileOperation(
        IAuthorizationTask authorizationTask,
        IGetFileInfoTask getFileInfoTask,
        IGetFileTask getFileTask,
        ISendNotificationCommandTask sendNotificationCommandTask,
        ILogger<GetFileOperation> logger, 
        IPathBuilderTask pathBuilderTask)
    {
        _authorizationTask = authorizationTask;
        _getFileInfoTask = getFileInfoTask;
        _getFileTask = getFileTask;
        _sendNotificationCommandTask = sendNotificationCommandTask;
        _logger = logger;
        _pathBuilderTask = pathBuilderTask;
    }

    public async Task<GetFileOperationResponse> GetFileAsync(GetFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileGet).ConfigureAwait(false);

        var fileInfo = await _getFileInfoTask.GetAsync(request.FileCode).ConfigureAwait(false);

        var path = await _pathBuilderTask.BuildAsync(FolderName.PersistentStorage, request.FileCode, fileInfo.Name).ConfigureAwait(false);
        var stream = await _getFileTask.GetAsync(path).ConfigureAwait(false);

        await _sendNotificationCommandTask.SendAsync(new SendNotificationCommandTaskRequest(UpdateFileType.GetFile, new[] { fileInfo.Name })).ConfigureAwait(false);

        _logger.LogInformation($"File successfully received");

        return new GetFileOperationResponse(fileInfo.Name, stream);
    }
}