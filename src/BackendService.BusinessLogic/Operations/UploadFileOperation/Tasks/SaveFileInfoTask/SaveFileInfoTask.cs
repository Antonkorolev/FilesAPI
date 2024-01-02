using DatabaseContext.FileDb;
using DatabaseContext.FileDb.Models;
using File = DatabaseContext.FileDb.Models.File;

namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.SaveFileInfoTask;

public sealed class SaveFileInfoTask : ISaveFileInfoTask
{
    private readonly IFileDbContext _fileDbContext;

    public SaveFileInfoTask(IFileDbContext fileDbContext)
    {
        _fileDbContext = fileDbContext;
    }

    public async Task SaveInfoAsync(Guid fileCode, string userCode)
    {
        await _fileDbContext.Database.BeginTransactionAsync(CancellationToken.None);
        
        var file = await _fileDbContext.File.AddAsync(
                new File
                {
                    FileCode = fileCode
                })
            .ConfigureAwait(false);
        
        await _fileDbContext.FileChangeHistory.AddAsync(
                new FileChangeHistory
                {
                    FileId = file.Entity.FileId,
                    CreatedBy = userCode,
                    Modified = DateTime.UtcNow
                })
            .ConfigureAwait(false);

        await _fileDbContext.Database.CommitTransactionAsync(CancellationToken.None);
    }
}