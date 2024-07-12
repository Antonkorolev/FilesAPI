using DatabaseContext.FileDb;
using DatabaseContext.FileDb.Models;
using FileInfo = DatabaseContext.FileDb.Models.FileInfo;

namespace ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.SaveFileInfo;

public class SaveFileInfoTask : ISaveFileInfoTask
{
    private readonly IFileDbContext _context;

    public SaveFileInfoTask(IFileDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync(string fileCode, string userCode, string fileName, CancellationToken cancellationToken)
    {
        await _context.Database.BeginTransactionAsync(cancellationToken);

        var fileInfo = await _context.FileInfo.AddAsync(
                new FileInfo
                {
                    Code = fileCode,
                    Name = fileName
                },
                cancellationToken)
            .ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken);

        await _context.FileChangeHistory.AddAsync(
                new FileChangeHistory
                {
                    FileInfoId = fileInfo.Entity.FileInfoId,
                    CreatedBy = userCode,
                    Created = DateTime.UtcNow
                },
                cancellationToken)
            .ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken);

        await _context.Database.CommitTransactionAsync(cancellationToken);
    }
}