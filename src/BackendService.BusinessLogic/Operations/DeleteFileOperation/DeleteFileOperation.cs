using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Helpers;
using BackendService.BusinessLogic.Operations.DeleteFileOperation.Models;
using BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileInfoTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using BackendService.BusinessLogic.Tasks.DeleteFileTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.DeleteFileOperation;

public sealed class DeleteFileOperation : IDeleteFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IDeleteFileInfoTask _deleteFileInfoTask;
    private readonly IDeleteFileTask _deleteFileTask;
    private readonly IGetFileInfoTask _getFileInfoTask;
    private readonly ILogger<DeleteFileOperation> _logger;

    public DeleteFileOperation(
        IAuthorizationTask authorizationTask, 
        IDeleteFileInfoTask deleteFileInfoTask, 
        IDeleteFileTask deleteFileTask, 
        IGetFileInfoTask getFileInfoTask, 
        ILogger<DeleteFileOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _deleteFileInfoTask = deleteFileInfoTask;
        _deleteFileTask = deleteFileTask;
        _getFileInfoTask = getFileInfoTask;
        _logger = logger;
    }

    public async Task DeleteAsync(DeleteFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileDelete).ConfigureAwait(false);

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var fileInfo = await _getFileInfoTask.GetAsync(request.FileCode).ConfigureAwait(false);
        var path = PathBuilder.Build(fileInfo.Code, fileInfo.Name);

        _deleteFileTask.Delete(path);
        await _deleteFileInfoTask.DeleteFileAsync(fileInfo.FileInfoId, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation($"File with FileCode = '{request.FileCode}' successfully deleted");
    }
}