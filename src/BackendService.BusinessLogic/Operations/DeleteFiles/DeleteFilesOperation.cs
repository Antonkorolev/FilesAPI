using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Operations.DeleteFile.Tasks.DeleteFileInfo;
using BackendService.BusinessLogic.Operations.DeleteFiles.Models;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.DeleteFile;
using BackendService.BusinessLogic.Tasks.GetFileInfos;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.DeleteFiles;

public sealed class DeleteFilesOperation : IDeleteFilesOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IGetFileInfosTask _getFileInfosTask;
    private readonly IPathBuilderTask _pathBuilderTask;
    private readonly IDeleteFileTask _deleteFileTask;
    private readonly IDeleteFileInfoTask _deleteFileInfoTask;
    private readonly ISendNotificationCommandTask _sendNotificationCommandTask;
    private readonly ILogger<DeleteFilesOperation> _logger;

    public DeleteFilesOperation(
        IAuthorizationTask authorizationTask,
        IGetFileInfosTask getFileInfosTask,
        IPathBuilderTask pathBuilderTask,
        IDeleteFileTask deleteFileTask,
        IDeleteFileInfoTask deleteFileInfoTask,
        ISendNotificationCommandTask sendNotificationCommandTask,
        ILogger<DeleteFilesOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _getFileInfosTask = getFileInfosTask;
        _pathBuilderTask = pathBuilderTask;
        _deleteFileTask = deleteFileTask;
        _deleteFileInfoTask = deleteFileInfoTask;
        _sendNotificationCommandTask = sendNotificationCommandTask;
        _logger = logger;
    }

    public async Task DeleteAsync(DeleteFilesOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileDelete).ConfigureAwait(false);

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var fileInfos = await _getFileInfosTask.GetAsync(request.FileCodes).ConfigureAwait(false);

        foreach (var fileInfo in fileInfos.FileInfos)
        {
            var path = await _pathBuilderTask.BuildAsync(FolderName.PersistentStorage, fileInfo.Code, fileInfo.Name).ConfigureAwait(false);

            await _deleteFileTask.DeleteAsync(path).ConfigureAwait(false);
            await _deleteFileInfoTask.DeleteFileAsync(fileInfo.FileInfoId, cancellationToken).ConfigureAwait(false);
        }

        await _sendNotificationCommandTask.SendAsync(new SendNotificationCommandTaskRequest(UpdateFileType.DeleteFiles, fileInfos.FileInfos.Select(t => t.Name).ToArray())).ConfigureAwait(false);

        _logger.LogInformation($"Files with FileCodes = '{string.Join(", ", request.FileCodes)}' successfully deleted");
    }
}