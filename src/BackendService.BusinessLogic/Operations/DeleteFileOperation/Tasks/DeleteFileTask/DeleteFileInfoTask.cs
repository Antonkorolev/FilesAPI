using DatabaseContext.FileDb;
using DatabaseContext.FileDb.Models;
using FileInfo = DatabaseContext.FileDb.Models.FileInfo;

namespace BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileTask;

public sealed class DeleteFileInfoTask : IDeleteFileInfoTask
{
    private readonly IFileDbContext _fileDbContext;

    public DeleteFileInfoTask(IFileDbContext fileDbContext)
    {
        _fileDbContext = fileDbContext;
    }

    public async Task DeleteFileAsync(int fileId, CancellationToken cancellationToken)
    {
        await _fileDbContext.Database.BeginTransactionAsync(cancellationToken);

        _fileDbContext.FileChangeHistory.RemoveRange(new FileChangeHistory { FileId = fileId });
        _fileDbContext.FileInfo.Remove(new FileInfo { FileInfoId = fileId });

        await _fileDbContext.Database.CommitTransactionAsync(cancellationToken);
    }
}