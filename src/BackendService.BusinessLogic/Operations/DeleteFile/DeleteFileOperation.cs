using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Operations.DeleteFile.Models;
using BackendService.BusinessLogic.Operations.DeleteFile.Tasks.DeleteFileInfo;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.DeleteFile;
using BackendService.BusinessLogic.Tasks.GetFileInfo;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.DeleteFile;

public sealed class DeleteFileOperation : IDeleteFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IDeleteFileInfoTask _deleteFileInfoTask;
    private readonly IDeleteFileTask _deleteFileTask;
    private readonly IGetFileInfoTask _getFileInfoTask;
    private readonly ISendNotificationCommandTask _sendNotificationCommandTask;
    private readonly IPathBuilderTask _pathBuilderTask;
    private readonly ILogger<DeleteFileOperation> _logger;

    public DeleteFileOperation(
        IAuthorizationTask authorizationTask,
        IDeleteFileInfoTask deleteFileInfoTask,
        IDeleteFileTask deleteFileTask,
        IGetFileInfoTask getFileInfoTask,
        ISendNotificationCommandTask sendNotificationCommandTask,
        ILogger<DeleteFileOperation> logger, 
        IPathBuilderTask pathBuilderTask)
    {
        _authorizationTask = authorizationTask;
        _deleteFileInfoTask = deleteFileInfoTask;
        _deleteFileTask = deleteFileTask;
        _getFileInfoTask = getFileInfoTask;
        _sendNotificationCommandTask = sendNotificationCommandTask;
        _logger = logger;
        _pathBuilderTask = pathBuilderTask;
    }

    public async Task DeleteAsync(DeleteFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileDelete).ConfigureAwait(false);

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var fileInfo = await _getFileInfoTask.GetAsync(request.FileCode).ConfigureAwait(false);
        var path = await _pathBuilderTask.BuildAsync(FolderName.PersistentStorage, fileInfo.Code, fileInfo.Name).ConfigureAwait(false);

        _deleteFileTask.Delete(path);
        await _deleteFileInfoTask.DeleteFileAsync(fileInfo.FileInfoId, cancellationToken).ConfigureAwait(false);

        await _sendNotificationCommandTask.SendAsync(new SendNotificationCommandTaskRequest(UpdateFileType.DeleteFile, new[] { fileInfo.Name })).ConfigureAwait(false);

        _logger.LogInformation($"File with FileCode = '{request.FileCode}' successfully deleted");
    }
}