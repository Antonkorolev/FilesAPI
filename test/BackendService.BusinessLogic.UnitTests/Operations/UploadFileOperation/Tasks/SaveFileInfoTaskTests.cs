using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.SaveFileInfoTask;
using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations.UploadFileOperation.Tasks;

[TestClass]
public sealed class SaveFileInfoTaskTests : UnitTestsBase
{
    private ISaveFileInfoTask _saveFileInfoTask = default!;
    private IFileDbContext _fileDbContext = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _fileDbContext = CreateFileDbContext("SaveFileInfoTaskTestsDb");

        _saveFileInfoTask = new SaveFileInfoTask(_fileDbContext);
    }

    [TestMethod]
    public async Task SaveFileInfoTask_ExecuteSuccessfully()
    {
        await _saveFileInfoTask.SaveAsync(DefaultFileCode, DefaultUserCode, DefaultFileName, It.IsAny<CancellationToken>()).ConfigureAwait(false);

        var fileInfo = await _fileDbContext.FileInfo.FirstOrDefaultAsync().ConfigureAwait(false);
        Assert.IsNotNull(fileInfo);
        Assert.AreEqual(FileInfoId, fileInfo.FileInfoId);
        Assert.AreEqual(DefaultFileCode, fileInfo.Code);
        Assert.AreEqual(DefaultFileName, fileInfo.Name);

        var fileChangeHistory = await _fileDbContext.FileChangeHistory.FirstOrDefaultAsync().ConfigureAwait(false);
        Assert.IsNotNull(fileChangeHistory);
        Assert.AreEqual(FileChangeHistoryId, fileChangeHistory.FileChangeHistoryId);
        Assert.AreEqual(FileInfoId, fileChangeHistory.FileInfoId);
        Assert.AreEqual(DefaultUserCode, fileChangeHistory.CreatedBy);
        Assert.IsNotNull(fileChangeHistory.Created);
        Assert.AreEqual(CurrentDateTime.Date, fileChangeHistory.Created.Value.Date);
        Assert.IsNull(fileChangeHistory.ModifiedBy);
        Assert.IsNull(fileChangeHistory.Modified);
    }
    
    [DataRow(null, DefaultFileName)]
    [DataRow(DefaultFileCode, null)]
    [DataTestMethod]
    public async Task SaveFileInfoTask_WithoutParams_(string fileCode, string fileName)
    {
        await Assert.ThrowsExceptionAsync<DbUpdateException>(() => _saveFileInfoTask.SaveAsync(fileCode, DefaultUserCode, fileName, It.IsAny<CancellationToken>())).ConfigureAwait(false);
    }
}