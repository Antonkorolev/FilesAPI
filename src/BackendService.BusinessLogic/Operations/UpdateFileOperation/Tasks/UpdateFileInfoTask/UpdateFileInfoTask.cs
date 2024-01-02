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

    public async Task UpdateInfoAsync(Guid fileCode, string userCode, CancellationToken cancellationToken)
    {
        var file = await _context.File.FirstOrDefaultAsync(f => f.FileCode == fileCode, cancellationToken).ConfigureAwait(false);

        if (file == null)
            throw new Exception($"File by FileCode = '{fileCode}' not found in database");

        await _context.FileChangeHistory.AddAsync(
                new FileChangeHistory
                {
                    FileId = file.FileId,
                    ModifiedBy = userCode,
                    Modified = DateTime.UtcNow
                },
                cancellationToken)
            .ConfigureAwait(false);
    }
}