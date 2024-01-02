using BackendService.BusinessLogic.Exceptions;
using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;

namespace BackendService.BusinessLogic.Tasks.GetFileTask;

public sealed class GetFileIdTask : IGetFileIdTask
{
    private readonly IFileDbContext _context;

    public GetFileIdTask(IFileDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> GetFileIdAsync(Guid fileCode, CancellationToken cancellationToken)
    {
        var file = await _context.File.FirstOrDefaultAsync(f => f.FileCode == fileCode, cancellationToken).ConfigureAwait(false);

        if (file == null)
            throw new FileNotFoundInDbException();

        return file.FileId;
    }
}