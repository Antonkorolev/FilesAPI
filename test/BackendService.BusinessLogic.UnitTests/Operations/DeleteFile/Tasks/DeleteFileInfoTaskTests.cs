using BackendService.BusinessLogic.Operations.DeleteFile.Tasks.DeleteFileInfo;
using DatabaseContext.FileDb;
using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileInfo = DatabaseContext.FileDb.Models.FileInfo;

namespace BackendService.BusinessLogic.UnitTests.Operations.DeleteFile.Tasks;

[TestClass]
public sealed class DeleteFileInfoTaskTests : UnitTestsBase
{
    private IFileDbContext _fileDbContext = default!;
    private DeleteFileInfoTask _deleteFileInfoTask = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _fileDbContext = CreateFileDbContext();
        _deleteFileInfoTask = new DeleteFileInfoTask(_fileDbContext);
    }

    [TestMethod]
    public async Task DeleteFileInfoTask_ExecuteSuccessfully()
    {
        var fileInfos = new FileInfo
        {
            Code = DefaultFileCode,
            Name = DefaultFileName
        };

        var fileChangeHistories = new FileChangeHistory
        {
            FileInfoId = DefaultFileInfoId,
            Created = CurrentDateTime,
            CreatedBy = DefaultUserCode,
            Modified = CurrentDateTime,
            ModifiedBy = DefaultUserCode
        };

        await _fileDbContext.FileInfo.AddAsync(fileInfos).ConfigureAwait(false);
        await _fileDbContext.FileChangeHistory.AddAsync(fileChangeHistories).ConfigureAwait(false);
        await _fileDbContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

        await _deleteFileInfoTask.DeleteFileAsync(DefaultFileInfoId, CancellationToken.None).ConfigureAwait(false);

        var fileInfo = await _fileDbContext.FileInfo.FirstOrDefaultAsync().ConfigureAwait(false);
        Assert.IsNull(fileInfo);

        var fileChangeHistory = await _fileDbContext.FileChangeHistory.FirstOrDefaultAsync().ConfigureAwait(false);
        Assert.IsNull(fileChangeHistory);
    }

    [TestMethod]
    public async Task DeleteFileInfoTask_FileChangeHistoryNotExists_ExecuteSuccessfully()
    {
        var fileInfo = new FileInfo()
        {
            Code = DefaultFileCode,
            Name = DefaultFileName
        };

        await _fileDbContext.FileInfo.AddAsync(fileInfo).ConfigureAwait(false);
        await _fileDbContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

        await _deleteFileInfoTask.DeleteFileAsync(DefaultFileInfoId, CancellationToken.None).ConfigureAwait(false);

        var expectedFileInfo = await _fileDbContext.FileInfo.FirstOrDefaultAsync().ConfigureAwait(false);
        Assert.IsNull(expectedFileInfo);

        var expectedFileChangeHistory = await _fileDbContext.FileChangeHistory.FirstOrDefaultAsync().ConfigureAwait(false);
        Assert.IsNull(expectedFileChangeHistory);
    }

    [TestMethod]
    public async Task DeleteFileInfoTask_FileInfoNotExists_ThrowsInvalidOperationException()
    {
        var fileChangeHistory = new FileChangeHistory
        {
            FileInfoId = DefaultFileInfoId,
            Created = CurrentDateTime,
            CreatedBy = DefaultUserCode,
            Modified = CurrentDateTime,
            ModifiedBy = DefaultUserCode
        };

        await _fileDbContext.FileChangeHistory.AddAsync(fileChangeHistory).ConfigureAwait(false);
        await _fileDbContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _deleteFileInfoTask.DeleteFileAsync(DefaultFileInfoId, CancellationToken.None)).ConfigureAwait(false);
    }

    [TestMethod]
    public async Task DeleteFileInfoTask_AnyDataNotExists_ThrowsInvalidOperationException()
    {
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _deleteFileInfoTask.DeleteFileAsync(DefaultFileInfoId, CancellationToken.None)).ConfigureAwait(false);
    }
}