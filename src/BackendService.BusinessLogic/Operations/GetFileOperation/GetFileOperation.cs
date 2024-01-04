using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Helpers;
using BackendService.BusinessLogic.Operations.GetFileOperation.Models;
using BackendService.BusinessLogic.Operations.GetFileOperation.Tasks.GetFileTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.GetFileOperation;

public sealed class GetFileOperation : IGetFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IGetFileInfoTask _getFileInfoTask;
    private readonly IGetFileTask _getFileTask;
    private readonly ILogger<GetFileOperation> _logger;

    public GetFileOperation(
        IAuthorizationTask authorizationTask,
        IGetFileInfoTask getFileInfoTask, 
        IGetFileTask getFileTask, 
        ILogger<GetFileOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _getFileInfoTask = getFileInfoTask;
        _getFileTask = getFileTask;
        _logger = logger;
    }

    public async Task<Stream> GetFile(GetFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileGet).ConfigureAwait(false);

        var getFileInfoTaskResponse = await _getFileInfoTask.GetAsync(request.FileCode).ConfigureAwait(false);

        var path = PathBuilder.Build(request.FileCode.ToString(), getFileInfoTaskResponse.Name);
        var stream = _getFileTask.Get(path);

        _logger.LogInformation($"File successfully received");

        return stream;
    }
}