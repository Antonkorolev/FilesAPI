using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Operations.UploadFile.Models;
using BackendService.BusinessLogic.Operations.UploadFile.Tasks.SaveFileInfo;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.EnsurePathExists;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;
using BackendService.BusinessLogic.Tasks.WriteFile;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UploadFile;

public sealed class UploadFileOperation : IUploadFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IWriteFileTask _writeFileTask;
    private readonly ISaveFileInfoTask _saveFileInfoTask;
    private readonly IEnsurePathExistsTask _ensurePathExistsTask;
    private readonly ISendNotificationCommandTask _sendNotificationCommandTask;
    private readonly IPathBuilderTask _pathBuilderTask;
    private readonly ILogger<UploadFileOperation> _logger;

    public UploadFileOperation(
        IAuthorizationTask authorizationTask,
        IWriteFileTask writeFileTask,
        ISaveFileInfoTask saveFileInfoTask,
        IEnsurePathExistsTask ensurePathExistsTask,
        ISendNotificationCommandTask sendNotificationCommandTask,
        IPathBuilderTask pathBuilderTask,
        ILogger<UploadFileOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _writeFileTask = writeFileTask;
        _saveFileInfoTask = saveFileInfoTask;
        _ensurePathExistsTask = ensurePathExistsTask;
        _sendNotificationCommandTask = sendNotificationCommandTask;
        _pathBuilderTask = pathBuilderTask;
        _logger = logger;
    }

    public async Task<string> UploadAsync(UploadFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileCreation).ConfigureAwait(false);

        var fileCode = Guid.NewGuid().ToString();
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        var path = await _pathBuilderTask.BuildAsync(FolderName.PersistentStorage, fileCode, request.FileName).ConfigureAwait(false);

        await _ensurePathExistsTask.EnsureExistingAsync(path).ConfigureAwait(false);
        await _writeFileTask.WriteAsync(request.Stream, path, cancellationToken).ConfigureAwait(false);
        await _saveFileInfoTask.SaveAsync(fileCode, request.UserCode, request.FileName, cancellationToken).ConfigureAwait(false);

        await _sendNotificationCommandTask.SendAsync(new SendNotificationCommandTaskRequest(UpdateFileType.UploadFile, new[] { request.FileName })).ConfigureAwait(false);

        _logger.LogInformation($"File with FileCode = '{fileCode}' successfully saved");

        return fileCode;
    }
}