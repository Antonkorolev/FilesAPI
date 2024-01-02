using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Helpers;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Models;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.SaveFileInfoTask;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.WriteFileTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UploadFileOperation;

public sealed class UploadFileOperation : IUploadFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IWriteFileTask _writeFileTask;
    private readonly ISaveFileInfoTask _saveFileInfoTask;
    private readonly ILogger _logger;

    public UploadFileOperation(IAuthorizationTask authorizationTask, IWriteFileTask writeFileTask, ISaveFileInfoTask saveFileInfoTask, ILogger logger)
    {
        _authorizationTask = authorizationTask;
        _writeFileTask = writeFileTask;
        _saveFileInfoTask = saveFileInfoTask;
        _logger = logger;
    }

    public async Task<Guid> UploadFileAsync(UploadFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileCreation).ConfigureAwait(false);

        var fileCode = Guid.NewGuid();

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var path = PathBuilder.Build(fileCode.ToString());

        await _writeFileTask.WriteAsync(request.FileStream, path, cancellationToken).ConfigureAwait(false);
        await _saveFileInfoTask.SaveInfoAsync(fileCode, request.UserCode, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation($"File with FileCode = '{fileCode}' successfully saved");

        return fileCode;
    }
}