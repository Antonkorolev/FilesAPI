using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Mappers;
using BackendService.BusinessLogic.Operations.GetFiles.Models;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfos;
using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFiles;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.PathsPreparation;
using BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand;
using BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand.Models;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.GetFiles;

public sealed class GetFilesOperation : IGetFilesOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IGetFileInfosTask _getFileInfosTask;
    private readonly IGetFilesTask _getFilesTask;
    private readonly IPathsPreparationTask _pathsPreparationTask;
    private readonly ISendUpdateFilesCommandTask _sendUpdateFilesCommandTask;
    private readonly ILogger<GetFilesOperation> _logger;

    public GetFilesOperation(
        IAuthorizationTask authorizationTask,
        IGetFileInfosTask getFileInfosTask,
        IGetFilesTask getFilesTask,
        IPathsPreparationTask pathsPreparationTask,
        ISendUpdateFilesCommandTask sendUpdateFilesCommandTask,
        ILogger<GetFilesOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _getFileInfosTask = getFileInfosTask;
        _getFilesTask = getFilesTask;
        _pathsPreparationTask = pathsPreparationTask;
        _sendUpdateFilesCommandTask = sendUpdateFilesCommandTask;
        _logger = logger;
    }

    public async Task<byte[]> GetFilesAsync(GetFilesOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileGet).ConfigureAwait(false);

        var getFileInfosTaskResponse = await _getFileInfosTask.GetAsync(request.FileCodes).ConfigureAwait(false);

        var pathsPreparationTaskResponse = _pathsPreparationTask.PreparePaths(getFileInfosTaskResponse.ToPathsPreparationTaskRequest());
        var byteArray = _getFilesTask.Get(pathsPreparationTaskResponse.ToGetFilesTaskRequest());

        await _sendUpdateFilesCommandTask.SendAsync(new SendUpdateFilesCommandTaskRequest(UpdateFileType.GetFiles, getFileInfosTaskResponse.FileInfos.Select(t => t.Name).ToArray())).ConfigureAwait(false);

        _logger.LogInformation($"Files successfully received");

        return byteArray;
    }
}