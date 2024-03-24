using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Helpers;
using BackendService.BusinessLogic.Operations.UpdateFile.Models;
using BackendService.BusinessLogic.Operations.UpdateFile.Tasks.UpdateFile;
using BackendService.BusinessLogic.Operations.UpdateFile.Tasks.UpdateFileInfo;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.DeleteFile;
using BackendService.BusinessLogic.Tasks.GetFileInfo;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UpdateFile;

public sealed class UpdateFileOperation : IUpdateFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IUpdateFileTask _updateFileTask;
    private readonly IUpdateFileInfoTask _updateFileInfoTask;
    private readonly IGetFileInfoTask _getFileInfoTask;
    private readonly IDeleteFileTask _deleteFileTask;
    private readonly ILogger<UpdateFileOperation> _logger;

    public UpdateFileOperation(
        IUpdateFileTask updateFileTask,
        IUpdateFileInfoTask updateFileInfoTask,
        IAuthorizationTask authorizationTask,
        IGetFileInfoTask getFileInfoTask,
        IDeleteFileTask deleteFileTask,
        ILogger<UpdateFileOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _updateFileTask = updateFileTask;
        _updateFileInfoTask = updateFileInfoTask;
        _getFileInfoTask = getFileInfoTask;
        _deleteFileTask = deleteFileTask;
        _logger = logger;
    }

    public async Task UpdateAsync(UpdateFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileUpdate).ConfigureAwait(false);

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var fileInfo = await _getFileInfoTask.GetAsync(request.FileCode).ConfigureAwait(false);
        var oldFilePath = PathBuilder.Build(fileInfo.Code, fileInfo.Name);
        _deleteFileTask.Delete(oldFilePath);

        var newFilePath = PathBuilder.Build(request.FileCode, request.FileName);
        await _updateFileTask.UpdateAsync(request.Stream, newFilePath, cancellationToken).ConfigureAwait(false);
        await _updateFileInfoTask.UpdateInfoAsync(fileInfo.FileInfoId, request.FileName, request.UserCode, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation($"File by FileCode = '{request.FileCode}' successfully updated");
    }
}