using DatabaseContext.FileDb;
using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileInfoTask;

public sealed class UpdateFileInfoTask : IUpdateFileInfoTask
{
    private readonly IFileDbContext _context;

    public UpdateFileInfoTask(IFileDbContext context)
    {
        _context = context;
    }

    public async Task UpdateInfoAsync(int fileId, Guid fileCode, string fileName, string userCode, CancellationToken cancellationToken)
    {
        await _context.Database.BeginTransactionAsync(cancellationToken);

        var file = await _context.FileInfo.FirstAsync(f => f.FileInfoId == fileId, cancellationToken).ConfigureAwait(false);
        file.Name = fileName;

        await _context.FileChangeHistory.AddAsync(
                new FileChangeHistory
                {
                    FileId = fileId,
                    ModifiedBy = userCode,
                    Modified = DateTime.UtcNow
                },
                cancellationToken)
            .ConfigureAwait(false);

        await _context.SaveChangesAsync(cancellationToken);
        await _context.Database.CommitTransactionAsync(cancellationToken);
    }
}