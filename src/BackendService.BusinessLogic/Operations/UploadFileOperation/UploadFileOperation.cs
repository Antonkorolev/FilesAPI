using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Helpers;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Models;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.SaveFileInfoTask;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.WriteFileTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using BackendService.BusinessLogic.Tasks.EnsurePathExistsTask;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UploadFileOperation;

public sealed class UploadFileOperation : IUploadFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IWriteFileTask _writeFileTask;
    private readonly ISaveFileInfoTask _saveFileInfoTask;
    private readonly IEnsurePathExistsTask _ensurePathExistsTask;
    private readonly ILogger<UploadFileOperation> _logger;

    public UploadFileOperation(
        IAuthorizationTask authorizationTask,
        IWriteFileTask writeFileTask,
        ISaveFileInfoTask saveFileInfoTask,
        IEnsurePathExistsTask ensurePathExistsTask,
        ILogger<UploadFileOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _writeFileTask = writeFileTask;
        _saveFileInfoTask = saveFileInfoTask;
        _ensurePathExistsTask = ensurePathExistsTask;
        _logger = logger;
    }

    public async Task<string> UploadAsync(UploadFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileCreation).ConfigureAwait(false);

        var fileCode = Guid.NewGuid().ToString();
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        var path = PathBuilder.Build(fileCode, request.FileName);

        _ensurePathExistsTask.EnsureExisting(path);
        await _writeFileTask.WriteAsync(request.Stream, path, cancellationToken).ConfigureAwait(false);
        await _saveFileInfoTask.SaveAsync(fileCode, request.UserCode, request.FileName, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation($"File with FileCode = '{fileCode}' successfully saved");

        return fileCode;
    }
}