using BackendService.BusinessLogic.Exceptions;
using BackendService.BusinessLogic.Tasks.GetFileInfo.Models;
using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;

namespace BackendService.BusinessLogic.Tasks.GetFileInfo;

public sealed class GetFileInfoTask : IGetFileInfoTask
{
    private readonly IFileDbContext _context;

    public GetFileInfoTask(IFileDbContext context)
    {
        _context = context;
    }
    
    public async Task<GetFileInfoTaskResponse> GetAsync(string fileCode)
    {
        var fileInfo = await _context.FileInfo.FirstOrDefaultAsync(f => f.Code == fileCode).ConfigureAwait(false);

        if (fileInfo == null)
            throw new FileInfoNotFoundException();

        return new GetFileInfoTaskResponse(fileInfo.FileInfoId, fileInfo.Code, fileInfo.Name);
    }
}