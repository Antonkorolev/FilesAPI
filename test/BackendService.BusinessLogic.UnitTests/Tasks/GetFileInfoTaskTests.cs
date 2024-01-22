using BackendService.BusinessLogic.Exceptions;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask;
using DatabaseContext.FileDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileInfo = DatabaseContext.FileDb.Models.FileInfo;

namespace BackendService.BusinessLogic.UnitTests.Tasks;

[TestClass]
public sealed class GetFileInfoTaskTests : UnitTestsBase
{
    private IGetFileInfoTask _getFileInfoTask = default!;
    private IFileDbContext _fileDbContext = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _fileDbContext = CreateFileDbContext();
        _getFileInfoTask = new GetFileInfoTask(_fileDbContext);
    }

    [TestMethod]
    public async Task GetFileInfoTask_ReturnsFileInfoNotFoundException()
    {
        var exception = await Assert.ThrowsExceptionAsync<FileInfoNotFoundException>(() => _getFileInfoTask.GetAsync(DefaultFileCode));

        Assert.AreEqual("FileInfo not found in database", exception.Message);
    }

    [TestMethod]
    public async Task GetFileInfoTask_ReturnsGetFileInfoTaskResponse()
    {
        var entities = new List<FileInfo>()
        {
            new()
            {
                FileInfoId = DefaultFileInfoId,
                Code = DefaultFileCode,
                Name = DefaultFileName
            },
            new()
            {
                FileInfoId = 2,
                Code = $"{DefaultFileCode}2",
                Name = $"{DefaultFileName}2"
            }
        };

        await _fileDbContext.FileInfo.AddRangeAsync(entities).ConfigureAwait(false);
        await _fileDbContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

        var getFileInfoTaskResponse = await _getFileInfoTask.GetAsync(DefaultFileCode).ConfigureAwait(false);

        Assert.AreEqual(1, getFileInfoTaskResponse.FileInfoId);
        Assert.AreEqual(DefaultFileCode, getFileInfoTaskResponse.Code);
        Assert.AreEqual(DefaultFileName, getFileInfoTaskResponse.Name);
    }
}