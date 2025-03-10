using BackendService.BusinessLogic.Operations.UpdateFile.Tasks.UpdateFileInfo;
using DatabaseContext.FileDb;
using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileInfo = DatabaseContext.FileDb.Models.FileInfo;

namespace BackendService.BusinessLogic.UnitTests.Operations.UpdateFileOperation.Tasks;

[TestClass]
public sealed class UpdateFileInfoTaskTests : UnitTestsBase
{
    private UpdateFileInfoTask _updateFileInfoTask = default!;
    private IFileDbContext _fileDbContext = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _fileDbContext = CreateFileDbContext();
        _updateFileInfoTask = new UpdateFileInfoTask(_fileDbContext);
    }

    [TestMethod]
    public async Task UpdateFileInfoTask_ExecuteSuccessfully()
    {
        await _fileDbContext.FileInfo.AddAsync(
                new FileInfo
                {
                    Code = DefaultFileCode,
                    Name = DefaultFileName
                })
            .ConfigureAwait(false);

        await _fileDbContext.FileChangeHistory.AddAsync(
                new FileChangeHistory
                {
                    FileInfoId = DefaultFileInfoId,
                    CreatedBy = DefaultUserCode,
                    Created = CurrentDateTime
                })
            .ConfigureAwait(false);
        await _fileDbContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

        await _updateFileInfoTask.UpdateInfoAsync(DefaultFileInfoId, NewFileName, NewUserCode, CancellationToken.None).ConfigureAwait(false);

        var fileInfo = await _fileDbContext.FileInfo.FirstOrDefaultAsync(f => f.FileInfoId == DefaultFileInfoId).ConfigureAwait(false);

        Assert.IsNotNull(fileInfo);
        Assert.AreEqual(DefaultFileCode, fileInfo.Code);
        Assert.AreEqual(NewFileName, fileInfo.Name);

        var fileChangeHistories = await _fileDbContext.FileChangeHistory.Where(f => f.FileInfoId == DefaultFileInfoId).ToArrayAsync().ConfigureAwait(false);

        Assert.AreEqual(2, fileChangeHistories.Length);

        var firstFileChangeHistory = fileChangeHistories.First();

        Assert.AreEqual(DefaultUserCode, firstFileChangeHistory.CreatedBy);
        Assert.AreEqual(CurrentDateTime.Date, firstFileChangeHistory.Created.GetValueOrDefault().Date);
        Assert.IsNull(firstFileChangeHistory.ModifiedBy);
        Assert.IsNull(firstFileChangeHistory.Modified);

        var secondFileChangeHistory = fileChangeHistories.Last();

        Assert.IsNull(secondFileChangeHistory.CreatedBy);
        Assert.IsNull(secondFileChangeHistory.Created);
        Assert.AreEqual(NewUserCode, secondFileChangeHistory.ModifiedBy);
        Assert.AreEqual(CurrentDateTime.Date, secondFileChangeHistory.Modified.GetValueOrDefault().Date);
    }

    [DataRow(null, NewUserCode, "FileName should be set")]
    [DataRow("", NewUserCode, "FileName should be set")]
    [DataTestMethod]
    public async Task UpdateFileInfoTask_ParamsHaveNullOrEmptyValues_ShouldBeFail(string fileName, string userCode, string errorMessage)
    {
        var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _updateFileInfoTask.UpdateInfoAsync(DefaultFileInfoId, fileName, userCode, CancellationToken.None)).ConfigureAwait(false);

        Assert.AreEqual(errorMessage, exception.Message);
    }
}