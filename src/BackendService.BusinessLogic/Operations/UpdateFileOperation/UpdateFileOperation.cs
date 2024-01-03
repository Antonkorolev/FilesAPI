using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Extensions;
using BackendService.BusinessLogic.Helpers;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Models;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileInfoTask;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.WriteFileTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UpdateFileOperation;

public sealed class UpdateFileOperation : IUpdateFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IWriteFileTask _writeFileTask;
    private readonly IUpdateFileInfoTask _updateFileInfoTask;
    private readonly IGetFileInfoTask _getFileInfoTask;
    private readonly ILogger _logger;

    public UpdateFileOperation(IWriteFileTask writeFileTask, IUpdateFileInfoTask updateFileInfoTask, IAuthorizationTask authorizationTask, IGetFileInfoTask getFileInfoTask, ILogger logger)
    {
        _authorizationTask = authorizationTask;
        _writeFileTask = writeFileTask;
        _updateFileInfoTask = updateFileInfoTask;
        _getFileInfoTask = getFileInfoTask;
        _logger = logger;
    }

    public async Task UpdateFileAsync(UpdateFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileUpdate).ConfigureAwait(false);

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var fileStream = request.Stream.ToFileStream();
        var path = PathBuilder.Build(request.FileCode.ToString(), fileStream.Name);
        var fileInfo = await _getFileInfoTask.GetAsync(request.FileCode).ConfigureAwait(false);

        await _writeFileTask.WriteAsync(fileStream, path, cancellationToken).ConfigureAwait(false);
        await _updateFileInfoTask.UpdateInfoAsync(fileInfo.FileInfoId, fileInfo.Code, fileInfo.Name, request.UserCode, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation($"File by FileCode = '{request.FileCode}' successfully updated");
    }
}