using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Operations.UpdateFiles.Models;
using BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.Models;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.EnsurePathExists;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;
using BackendService.BusinessLogic.Tasks.WriteFile;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UpdateFiles;

public sealed class UpdateFilesOperation : IUpdateFilesOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IEnsurePathExistsTask _ensurePathExistsTask;
    private readonly IWriteFileTask _writeFileTask;
    private readonly ISendNotificationCommandTask _sendNotificationCommandTask;
    private readonly IPathBuilderTask _pathBuilderTask;
    private readonly ILogger<UpdateFilesOperation> _logger;

    public UpdateFilesOperation(
        IAuthorizationTask authorizationTask,
        IEnsurePathExistsTask ensurePathExistsTask,
        IWriteFileTask writeFileTask,
        ISendNotificationCommandTask sendNotificationCommandTask,
        IPathBuilderTask pathBuilderTask,
        ILogger<UpdateFilesOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _ensurePathExistsTask = ensurePathExistsTask;
        _writeFileTask = writeFileTask;
        _sendNotificationCommandTask = sendNotificationCommandTask;
        _pathBuilderTask = pathBuilderTask;
        _logger = logger;
    }

    public async Task UpdateAsync(UpdateFilesOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileUpdate).ConfigureAwait(false);

        var sendUpdateFilesCommandTaskRequest = new SendUpdateFilesCommandTaskRequest(new List<SendUpdateFilesData>());

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        foreach (var updateFileData in request.UpdateFileData)
        {
            var path = await _pathBuilderTask.BuildAsync(FolderName.TemporaryStorage, updateFileData.FileCode, updateFileData.FileName).ConfigureAwait(false);

            await _ensurePathExistsTask.EnsureExistingAsync(path).ConfigureAwait(false);
            await _writeFileTask.WriteAsync(updateFileData.Stream, path, cancellationToken).ConfigureAwait(false);

            sendUpdateFilesCommandTaskRequest.SendUpdateFilesData.Add(new SendUpdateFilesData(updateFileData.FileName, updateFileData.FileCode));
        }

        await _sendNotificationCommandTask.SendAsync(new SendNotificationCommandTaskRequest(UpdateFileType.UpdateFiles, request.UpdateFileData.Select(t => t.FileName))).ConfigureAwait(false);

        _logger.LogInformation($"Files with file codes = '{request.UpdateFileData.Select(t => t.FileCode)}', sent for update to processing");
    }
}