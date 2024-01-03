using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Extensions;
using BackendService.BusinessLogic.Helpers;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Models;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFileInfosTask;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.GetFilesOperation;

public sealed class GetFilesOperation : IGetFilesOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IGetFileInfosTask _getFileInfosTask;
    private readonly IGetFilesTask _getFilesTask;
    private readonly ILogger _logger;

    public GetFilesOperation(IAuthorizationTask authorizationTask, IGetFileInfosTask getFileInfosTask, IGetFilesTask getFilesTask, ILogger logger)
    {
        _authorizationTask = authorizationTask;
        _getFileInfosTask = getFileInfosTask;
        _getFilesTask = getFilesTask;
        _logger = logger;
    }

    public async Task<IEnumerable<Stream>> GetFiles(GetFilesOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileGet).ConfigureAwait(false);

        var getFileInfosTaskResponse = await _getFileInfosTask.GetAsync(request.FileCodes).ConfigureAwait(false);

        var paths = PathBuilder.Build(getFileInfosTaskResponse.ToPathBuilderRequest());
        var streams = _getFilesTask.Get(paths);

        _logger.LogInformation($"Files successfully received");

        return streams;
    }
}