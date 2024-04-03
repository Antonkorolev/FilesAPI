using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Helpers;
using BackendService.BusinessLogic.Operations.UploadFile.Models;
using BackendService.BusinessLogic.Operations.UploadFile.Tasks.EnsurePathExists;
using BackendService.BusinessLogic.Operations.UploadFile.Tasks.GenerateFileCode;
using BackendService.BusinessLogic.Operations.UploadFile.Tasks.SaveFileInfo;
using BackendService.BusinessLogic.Operations.UploadFile.Tasks.WriteFile;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand;
using BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand.Models;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UploadFile;

public sealed class UploadFileOperation : IUploadFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IWriteFileTask _writeFileTask;
    private readonly ISaveFileInfoTask _saveFileInfoTask;
    private readonly IEnsurePathExistsTask _ensurePathExistsTask;
    private readonly IGenerateFileCodeTask _generateFileCodeTask;
    private readonly ISendUpdateFilesCommandTask _sendUpdateFilesCommandTask;
    private readonly ILogger<UploadFileOperation> _logger;

    public UploadFileOperation(
        IAuthorizationTask authorizationTask,
        IWriteFileTask writeFileTask,
        ISaveFileInfoTask saveFileInfoTask,
        IEnsurePathExistsTask ensurePathExistsTask,
        IGenerateFileCodeTask generateFileCodeTask,
        ISendUpdateFilesCommandTask sendUpdateFilesCommandTask,
        ILogger<UploadFileOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _writeFileTask = writeFileTask;
        _saveFileInfoTask = saveFileInfoTask;
        _ensurePathExistsTask = ensurePathExistsTask;
        _generateFileCodeTask = generateFileCodeTask;
        _sendUpdateFilesCommandTask = sendUpdateFilesCommandTask;
        _logger = logger;
    }

    public async Task<string> UploadAsync(UploadFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileCreation).ConfigureAwait(false);

        var fileCode = await _generateFileCodeTask.GenerateAsync(request.Stream).ConfigureAwait(false);
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        var path = PathBuilder.Build(fileCode, request.FileName);

        _ensurePathExistsTask.EnsureExisting(path);
        await _writeFileTask.WriteAsync(request.Stream, path, cancellationToken).ConfigureAwait(false);
        await _saveFileInfoTask.SaveAsync(fileCode, request.UserCode, request.FileName, cancellationToken).ConfigureAwait(false);
        
        await _sendUpdateFilesCommandTask.SendAsync(new SendUpdateFilesCommandTaskRequest(UpdateFileType.UploadFile, new[] { request.FileName })).ConfigureAwait(false);

        _logger.LogInformation($"File with FileCode = '{fileCode}' successfully saved");

        return fileCode;
    }
}