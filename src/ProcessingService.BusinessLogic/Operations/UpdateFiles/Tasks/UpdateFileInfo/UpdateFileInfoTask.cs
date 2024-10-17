using DatabaseContext.FileDb;
using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;

namespace ProcessingService.BusinessLogic.Operations.UpdateFiles.Tasks.UpdateFileInfo;

public sealed class UpdateFileInfoTask : IUpdateFileInfoTask
{
    private readonly IFileDbContext _context;

    public UpdateFileInfoTask(IFileDbContext context)
    {
        _context = context;
    }

    public async Task UpdateInfoAsync(int fileId, string fileName, string userCode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("FileName should be set");

        var file = await _context.FileInfo.FirstAsync(f => f.FileInfoId == fileId, cancellationToken).ConfigureAwait(false);
        file.Name = fileName;

        await _context.FileChangeHistory.AddAsync(
                new FileChangeHistory
                {
                    FileInfoId = fileId,
                    ModifiedBy = userCode,
                    Modified = DateTime.UtcNow
                },
                cancellationToken)
            .ConfigureAwait(false);

        await _context.SaveChangesAsync(cancellationToken);
    }
}