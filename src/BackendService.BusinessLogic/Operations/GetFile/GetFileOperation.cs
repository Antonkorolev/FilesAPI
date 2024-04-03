using BackendService.BusinessLogic.Constants;
using BackendService.BusinessLogic.Helpers;
using BackendService.BusinessLogic.Operations.GetFile.Models;
using BackendService.BusinessLogic.Operations.GetFile.Tasks.GetFile;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.GetFileInfo;
using BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand;
using BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand.Models;
using Common;
using Microsoft.Extensions.Logging;

namespace BackendService.BusinessLogic.Operations.GetFile;

public sealed class GetFileOperation : IGetFileOperation
{
    private readonly IAuthorizationTask _authorizationTask;
    private readonly IGetFileInfoTask _getFileInfoTask;
    private readonly IGetFileTask _getFileTask;
    private readonly ISendUpdateFilesCommandTask _sendUpdateFilesCommandTask;
    private readonly ILogger<GetFileOperation> _logger;

    public GetFileOperation(
        IAuthorizationTask authorizationTask,
        IGetFileInfoTask getFileInfoTask,
        IGetFileTask getFileTask,
        ISendUpdateFilesCommandTask sendUpdateFilesCommandTask,
        ILogger<GetFileOperation> logger)
    {
        _authorizationTask = authorizationTask;
        _getFileInfoTask = getFileInfoTask;
        _getFileTask = getFileTask;
        _sendUpdateFilesCommandTask = sendUpdateFilesCommandTask;
        _logger = logger;
    }

    public async Task<GetFileOperationResponse> GetFileAsync(GetFileOperationRequest request)
    {
        await _authorizationTask.UserAuthorizationAsync(request.UserCode, Permissions.FileGet).ConfigureAwait(false);

        var fileInfo = await _getFileInfoTask.GetAsync(request.FileCode).ConfigureAwait(false);

        var path = PathBuilder.Build(request.FileCode, fileInfo.Name);
        var stream = await _getFileTask.GetAsync(path).ConfigureAwait(false);

        await _sendUpdateFilesCommandTask.SendAsync(new SendUpdateFilesCommandTaskRequest(UpdateFileType.GetFile, new[] { fileInfo.Name })).ConfigureAwait(false);

        _logger.LogInformation($"File successfully received");

        return new GetFileOperationResponse(fileInfo.Name, stream);
    }
}