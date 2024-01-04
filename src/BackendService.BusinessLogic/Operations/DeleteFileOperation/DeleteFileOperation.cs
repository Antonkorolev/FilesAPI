using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Helpers;
using BackendService.BusinessLogic.Operations.DeleteFileOperation.Models;
using BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileFromDbTask;
using BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.DeleteFileOperation;

public sealed class DeleteFileOperation : IDeleteFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IDeleteFileInfoTask _deleteFileInfoTask;
    private readonly IDeleteFileFromDbTask _deleteFileFromDbTask;
    private readonly IGetFileInfoTask _getFileInfoTask;
    private readonly ILogger _logger;

    public DeleteFileOperation(IAuthorizationTask authorizationTask, IDeleteFileInfoTask deleteFileInfoTask, IDeleteFileFromDbTask deleteFileFromDbTask, IGetFileInfoTask getFileInfoTask, ILogger logger)
    {
        _authorizationTask = authorizationTask;
        _deleteFileInfoTask = deleteFileInfoTask;
        _deleteFileFromDbTask = deleteFileFromDbTask;
        _getFileInfoTask = getFileInfoTask;
        _logger = logger;
    }

    public async Task DeleteAsync(DeleteFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileDelete).ConfigureAwait(false);

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var getFileInfoTaskResponse = await _getFileInfoTask.GetAsync(request.FileCode).ConfigureAwait(false);
        var path = PathBuilder.Build(getFileInfoTaskResponse.Code.ToString(), getFileInfoTaskResponse.Name);
        

        await _deleteFileInfoTask.DeleteFileAsync(getFileInfoTaskResponse.FileInfoId, cancellationToken).ConfigureAwait(false);
        _deleteFileFromDbTask.Delete(path);

        _logger.LogInformation($"File with FileCode = '{request.FileCode}' successfully deleted");
    }
}