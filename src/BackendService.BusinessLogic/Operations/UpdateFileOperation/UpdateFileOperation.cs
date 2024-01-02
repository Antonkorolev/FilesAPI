using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Helpers;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Models;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileInfoTask;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.WriteFileTask;
using BackendService.BusinessLogic.Tasks;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.UpdateFileOperation;

public sealed class UpdateFileOperation : IUpdateFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IUpdateFileTask _updateFileTask;
    private readonly IUpdateFileInfoTask _updateFileInfoTask;
    private readonly ILogger _logger;

    public UpdateFileOperation(IUpdateFileTask updateFileTask, IUpdateFileInfoTask updateFileInfoTask, IAuthorizationTask authorizationTask, ILogger logger)
    {
        _authorizationTask = authorizationTask;
        _updateFileTask = updateFileTask;
        _updateFileInfoTask = updateFileInfoTask;
        _logger = logger;
    }

    public async Task UpdateFileAsync(UpdateFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileUpdate).ConfigureAwait(false);
        
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var path = PathBuilder.Build(request.FileCode.ToString());

        await _updateFileTask.UpdateAsync(request.FileStream, path, cancellationToken).ConfigureAwait(false);
        await _updateFileInfoTask.UpdateInfoAsync(request.FileCode, request.UserCode, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation($"File by FileCode = '{request.FileCode}' successfully updated");
    }
}