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

    public async Task UpdateInfoAsync(int fileId, Guid fileCode, string userCode, CancellationToken cancellationToken)
    {
        await _context.FileChangeHistory.AddAsync(
                new FileChangeHistory
                {
                    FileId = fileId,
                    ModifiedBy = userCode,
                    Modified = DateTime.UtcNow
                },
                cancellationToken)
            .ConfigureAwait(false);
    }
}