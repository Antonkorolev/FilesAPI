using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;
using ProcessingService.BusinessLogic.Tasks.GetFileInfos.Models;
using FileInfo = ProcessingService.BusinessLogic.Tasks.GetFileInfos.Models.FileInfo;

namespace ProcessingService.BusinessLogic.Tasks.GetFileInfos;

public sealed class GetFileInfosTask : IGetFileInfosTask
{
    private readonly IFileDbContext _context;

    public GetFileInfosTask(IFileDbContext context)
    {
        _context = context;
    }

    public async Task<GetFileInfosTaskResponse> GetAsync(IEnumerable<string> fileCodes)
    {
        var fileInfos = await _context.FileInfo
            .Where(f => fileCodes.Contains(f.Code))
            .Select(f => new FileInfo(f.FileInfoId, f.Code, f.Name))
            .ToArrayAsync()
            .ConfigureAwait(false);

        var fileCodesArray = fileCodes as string[] ?? fileCodes.ToArray();

        if (fileInfos.Length != fileCodesArray.Length)
            throw new Exception($"Expected FileInfo count: {fileCodesArray.Length}, actual FileInfo count: {fileInfos.Length}");

        return new GetFileInfosTaskResponse(fileInfos);
    }
}