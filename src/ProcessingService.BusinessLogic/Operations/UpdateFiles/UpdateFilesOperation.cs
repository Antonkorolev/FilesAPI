using Common;
using Microsoft.Extensions.Logging;
using ProcessingService.BusinessLogic.Operations.UpdateFiles.Models;
using ProcessingService.BusinessLogic.Operations.UpdateFiles.Tasks.UpdateFile;
using ProcessingService.BusinessLogic.Operations.UpdateFiles.Tasks.UpdateFileInfo;
using ProcessingService.BusinessLogic.Tasks.DeleteFile;
using ProcessingService.BusinessLogic.Tasks.EnsurePathExists;
using ProcessingService.BusinessLogic.Tasks.GetFileInfos;
using ProcessingService.BusinessLogic.Tasks.PathBuilder;
using ProcessingService.BusinessLogic.Tasks.ReadFile;

namespace ProcessingService.BusinessLogic.Operations.UpdateFiles;

public sealed class UpdateFilesOperation : IUpdateFilesOperation
{
    private readonly IGetFileInfosTask _getFileInfosTask;
    private readonly IReadFileTask _readFileTask;
    private readonly IDeleteFileTask _deleteFileTask;
    private IPathBuilderTask _pathBuilderTask;
    private IEnsurePathExistsTask _ensurePathExistsTask;
    private readonly IUpdateFileInfoTask _updateFileInfoTask;
    private readonly IUpdateFileTask _updateFileTask;
    private readonly ILogger<UpdateFilesOperation> _logger;

    public UpdateFilesOperation(
        IGetFileInfosTask getFileInfosTask,
        IReadFileTask readFileTask,
        IDeleteFileTask deleteFileTask,
        IPathBuilderTask pathBuilderTask,
        IEnsurePathExistsTask ensurePathExistsTask,
        IUpdateFileInfoTask updateFileInfoTask,
        IUpdateFileTask updateFileTask,
        ILogger<UpdateFilesOperation> logger)
    {
        _getFileInfosTask = getFileInfosTask;
        _readFileTask = readFileTask;
        _deleteFileTask = deleteFileTask;
        _pathBuilderTask = pathBuilderTask;
        _ensurePathExistsTask = ensurePathExistsTask;
        _updateFileInfoTask = updateFileInfoTask;
        _updateFileTask = updateFileTask;
        _logger = logger;
    }

    public async Task UpdateFilesAsync(UpdateFilesOperationRequest request)
    {
        var getFileInfosTaskResponse = await _getFileInfosTask.GetAsync(request.UpdateFiles.Select(t => t.FileCode)).ConfigureAwait(false);

        foreach (var uploadFile in request.UpdateFiles)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            var fileInfo = getFileInfosTaskResponse.FileInfos.First(f => f.Code == uploadFile.FileCode);

            var oldFilePath = await _pathBuilderTask.BuildAsync(FolderName.PersistentStorage, fileInfo.Code, fileInfo.Name).ConfigureAwait(false);
            await _deleteFileTask.DeleteAsync(oldFilePath).ConfigureAwait(false);

            var temporaryStoragePath = await _pathBuilderTask.BuildAsync(FolderName.TemporaryStorage, uploadFile.FileCode, uploadFile.FileName).ConfigureAwait(false);
            var stream = await _readFileTask.ReadFileAsync(temporaryStoragePath).ConfigureAwait(false);

            var persistenceStoragePath = await _pathBuilderTask.BuildAsync(FolderName.PersistentStorage, uploadFile.FileCode, uploadFile.FileName).ConfigureAwait(false);
            await _ensurePathExistsTask.EnsureExistingAsync(persistenceStoragePath).ConfigureAwait(false);
            await _updateFileTask.UpdateAsync(stream, persistenceStoragePath, cancellationToken).ConfigureAwait(false);
            await _updateFileInfoTask.UpdateInfoAsync(fileInfo.FileInfoId, uploadFile.FileName, uploadFile.FileCode, cancellationToken).ConfigureAwait(false);

            await _deleteFileTask.DeleteAsync(temporaryStoragePath).ConfigureAwait(false);
        }

        _logger.LogInformation($"Files with file codes: [{string.Join(", ", request.UpdateFiles.Select(t => t.FileCode))}], successfully saved to persistence storage");
    }
}