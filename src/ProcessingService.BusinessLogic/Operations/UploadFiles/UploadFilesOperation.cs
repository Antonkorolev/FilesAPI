using Common;
using Microsoft.Extensions.Logging;
using ProcessingService.BusinessLogic.Constants;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Models;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.DeleteFilesFromTemporaryStorage;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.ReadFilesFromTemporaryStorage;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.SaveFileInfo;
using ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.WriteFileToPersistenceStorage;
using ProcessingService.BusinessLogic.Tasks.EnsurePathExists;
using ProcessingService.BusinessLogic.Tasks.PathBuilder;

namespace ProcessingService.BusinessLogic.Operations.UploadFiles;

public sealed class UploadFilesOperation : IUploadFilesOperation
{
    private readonly IReadFilesFromTemporaryStorageTask _readFilesFromTemporaryStorageTask;
    private readonly IWriteFileToPersistenceStorage _writeFileToPersistenceStorage;
    private readonly ISaveFileInfoTask _saveFileInfoTask;
    private readonly IDeleteFilesFromTemporaryStorageTask _deleteFilesFromTemporaryStorageTask;
    private IPathBuilderTask _pathBuilderTask;
    private IEnsurePathExistsTask _ensurePathExistsTask;
    private readonly ILogger<UploadFilesOperation> _logger;

    public UploadFilesOperation(
        IReadFilesFromTemporaryStorageTask readFilesFromTemporaryStorageTask,
        IWriteFileToPersistenceStorage writeFileToPersistenceStorage,
        ISaveFileInfoTask saveFileInfoTask,
        IDeleteFilesFromTemporaryStorageTask deleteFilesFromTemporaryStorageTask,
        ILogger<UploadFilesOperation> logger,
        IPathBuilderTask pathBuilderTask, 
        IEnsurePathExistsTask ensurePathExistsTask)
    {
        _readFilesFromTemporaryStorageTask = readFilesFromTemporaryStorageTask;
        _writeFileToPersistenceStorage = writeFileToPersistenceStorage;
        _saveFileInfoTask = saveFileInfoTask;
        _deleteFilesFromTemporaryStorageTask = deleteFilesFromTemporaryStorageTask;
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
            var stream = await _readFilesFromTemporaryStorageTask.RadFileAsync(temporaryStoragePath).ConfigureAwait(false);

            var persistenceStoragePath = await _pathBuilderTask.BuildAsync(FolderName.PersistentStorage, uploadFile.FileCode, uploadFile.FileName).ConfigureAwait(false);
            
            _ensurePathExistsTask.EnsureExisting(persistenceStoragePath);
            await _writeFileToPersistenceStorage.WriteAsync(stream, persistenceStoragePath, cancellationToken).ConfigureAwait(false);
            _deleteFilesFromTemporaryStorageTask.DeleteAsync(temporaryStoragePath);
            await _saveFileInfoTask.SaveAsync(uploadFile.FileCode, Logins.ProcessingUser, uploadFile.FileName, cancellationToken).ConfigureAwait(false);
        }

        _logger.LogInformation($"Files with file codes: [{string.Join(", ", request.UploadFiles.Select(t => t.FileCode))}], successfully saved to persistence storage");
    }
}