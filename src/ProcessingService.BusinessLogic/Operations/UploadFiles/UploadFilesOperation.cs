using Common;
using Microsoft.Extensions.Logging;
using ProcessingService.BusinessLogic.Constants;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Models;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.WriteFile;
using ProcessingService.BusinessLogic.Tasks.DeleteFile;
using ProcessingService.BusinessLogic.Tasks.EnsurePathExists;
using ProcessingService.BusinessLogic.Tasks.PathBuilder;
using ProcessingService.BusinessLogic.Tasks.ReadFile;
using ProcessingService.BusinessLogic.Tasks.SaveFileInfo;

namespace ProcessingService.BusinessLogic.Operations.UploadFiles;

public sealed class UploadFilesOperation : IUploadFilesOperation
{
    private readonly IReadFileTask _readFileTask;
    private readonly IWriteFileTask _writeFileTask;
    private readonly ISaveFileInfoTask _saveFileInfoTask;
    private readonly IDeleteFileTask _deleteFileTask;
    private IPathBuilderTask _pathBuilderTask;
    private IEnsurePathExistsTask _ensurePathExistsTask;
    private readonly ILogger<UploadFilesOperation> _logger;

    public UploadFilesOperation(
        IReadFileTask readFileTask,
        IWriteFileTask writeFileTask,
        ISaveFileInfoTask saveFileInfoTask,
        IDeleteFileTask deleteFileTask,
        ILogger<UploadFilesOperation> logger,
        IPathBuilderTask pathBuilderTask, 
        IEnsurePathExistsTask ensurePathExistsTask)
    {
        _readFileTask = readFileTask;
        _writeFileTask = writeFileTask;
        _saveFileInfoTask = saveFileInfoTask;
        _deleteFileTask = deleteFileTask;
        _logger = logger;
        _pathBuilderTask = pathBuilderTask;
        _ensurePathExistsTask = ensurePathExistsTask;
    }

    public async Task UploadFilesAsync(UploadFilesOperationRequest request)
    {
        foreach (var uploadFile in request.UploadFiles)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            var temporaryStoragePath = await _pathBuilderTask.BuildAsync(FolderName.TemporaryStorage, uploadFile.FileCode, uploadFile.FileName).ConfigureAwait(false);
            var stream = await _readFileTask.ReadFileAsync(temporaryStoragePath).ConfigureAwait(false);

            var persistenceStoragePath = await _pathBuilderTask.BuildAsync(FolderName.PersistentStorage, uploadFile.FileCode, uploadFile.FileName).ConfigureAwait(false);
            
            await _ensurePathExistsTask.EnsureExistingAsync(persistenceStoragePath).ConfigureAwait(false);
            await _writeFileTask.WriteAsync(stream, persistenceStoragePath, cancellationToken).ConfigureAwait(false);
            await _deleteFileTask.DeleteAsync(temporaryStoragePath).ConfigureAwait(false);
            await _saveFileInfoTask.SaveAsync(uploadFile.FileCode, Logins.ProcessingUser, uploadFile.FileName, cancellationToken).ConfigureAwait(false);
        }

        _logger.LogInformation($"Files with file codes: [{string.Join(", ", request.UploadFiles.Select(t => t.FileCode))}], successfully saved to persistence storage");
    }
}