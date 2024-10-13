using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Operations.UploadFiles.Models;
using BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand;
using BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand.Models;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.EnsurePathExists;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;
using BackendService.BusinessLogic.Tasks.WriteFile;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UploadFiles;

public sealed class UploadFilesOperation : IUploadFilesOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IWriteFileTask _writeFileTask;
    private readonly IEnsurePathExistsTask _ensurePathExistsTask;
    private readonly ISendUploadFilesCommandTask _sendUploadFilesCommandTask;
    private readonly ISendNotificationCommandTask _sendNotificationCommandTask;
    private readonly IPathBuilderTask _pathBuilderTask;
    private readonly ILogger<UploadFilesOperation> _logger;

    public UploadFilesOperation(
        IAuthorizationTask authorizationTask,
        IWriteFileTask writeFileTask,
        IEnsurePathExistsTask ensurePathExistsTask,
        ISendUploadFilesCommandTask sendUploadFilesCommandTask,
        ISendNotificationCommandTask sendNotificationCommandTask,
        ILogger<UploadFilesOperation> logger,
        IPathBuilderTask pathBuilderTask)
    {
        _authorizationTask = authorizationTask;
        _writeFileTask = writeFileTask;
        _ensurePathExistsTask = ensurePathExistsTask;
        _sendUploadFilesCommandTask = sendUploadFilesCommandTask;
        _logger = logger;
        _pathBuilderTask = pathBuilderTask;
        _sendNotificationCommandTask = sendNotificationCommandTask;
    }

    public async Task<IEnumerable<string>> UploadAsync(UploadFilesOperationRequest request)
    {
        var fileCodes = new List<string>();
        var sendUploadFilesCommandTaskRequest = new SendUploadFilesCommandTaskRequest(new List<SendUploadFilesData>());

        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileUpdate).ConfigureAwait(false);

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        foreach (var uploadFileData in request.UploadFileData)
        {
            var fileCode = Guid.NewGuid().ToString();
            var path = await _pathBuilderTask.BuildAsync(FolderName.TemporaryStorage, fileCode, uploadFileData.FileName).ConfigureAwait(false);

            await _ensurePathExistsTask.EnsureExistingAsync(path).ConfigureAwait(false);
            await _writeFileTask.WriteAsync(uploadFileData.Stream, path, cancellationToken).ConfigureAwait(false);

            fileCodes.Add(fileCode);
            sendUploadFilesCommandTaskRequest.SendUploadFilesData.Add(new SendUploadFilesData(uploadFileData.FileName, fileCode));
        }

        await _sendUploadFilesCommandTask.SendAsync(sendUploadFilesCommandTaskRequest).ConfigureAwait(false);
        await _sendNotificationCommandTask.SendAsync(new SendNotificationCommandTaskRequest(UpdateFileType.UploadFiles, request.UploadFileData.Select(t => t.FileName))).ConfigureAwait(false);

        _logger.LogInformation($"Files with file codes: [{string.Join(", ", fileCodes)}], successfully saved");

        return fileCodes;
    }
}