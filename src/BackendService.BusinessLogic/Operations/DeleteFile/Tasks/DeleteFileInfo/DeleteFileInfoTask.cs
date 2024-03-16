using DatabaseContext.FileDb;

namespace BackendService.BusinessLogic.Operations.DeleteFile.Tasks.DeleteFileInfo;

public sealed class DeleteFileInfoTask : IDeleteFileInfoTask
{
    private readonly IFileDbContext _context;

    public DeleteFileInfoTask(IFileDbContext context)
    {
        _context = context;
    }

    public async Task DeleteFileAsync(int fileInfoId, CancellationToken cancellationToken)
    {
        await _context.Database.BeginTransactionAsync(cancellationToken);

        _context.FileChangeHistory.RemoveRange(_context.FileChangeHistory.Where(f => f.FileInfoId == fileInfoId));
        await _context.SaveChangesAsync(cancellationToken);

        _context.FileInfo.Remove(_context.FileInfo.First(f => f.FileInfoId == fileInfoId));
        await _context.SaveChangesAsync(cancellationToken);

        await _context.Database.CommitTransactionAsync(cancellationToken);
    }
}