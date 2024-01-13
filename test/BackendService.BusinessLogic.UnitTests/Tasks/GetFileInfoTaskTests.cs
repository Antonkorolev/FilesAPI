using BackendService.BusinessLogic.Exceptions;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask;
using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.EntityFrameworkCore;
using FileInfo = DatabaseContext.FileDb.Models.FileInfo;

namespace BackendService.BusinessLogic.UnitTests.Tasks;

[TestClass]
public sealed class GetFileInfoTaskTests
{
    private readonly IGetFileInfoTask _getFileInfoTask;
    private readonly Mock<IFileDbContext> _fileDbContext;

    public GetFileInfoTaskTests()
    {
        _fileDbContext = new Mock<IFileDbContext>();
        _getFileInfoTask = new GetFileInfoTask(_fileDbContext.Object);
    }

    [TestMethod]
    public async Task GetFileInfoTask_ReturnsGetFileInfoTaskResponse()
    {
        const string code = "testCode";
        const string name = "testName";

        var entity = new List<FileInfo>()
        {
            new()
            {
                FileInfoId = 1,
                Code = code,
                Name = name
            },
            new()
            {
                FileInfoId = 2,
                Code = $"{code}2",
                Name = $"{name}2"
            }
        };

        _fileDbContext
            .Setup<DbSet<FileInfo>>(c => c.FileInfo)
            .ReturnsDbSet(entity);

        var getFileInfoTaskResponse = await _getFileInfoTask.GetAsync(code).ConfigureAwait(false);

        Assert.AreEqual(1, getFileInfoTaskResponse.FileInfoId);
        Assert.AreEqual(code, getFileInfoTaskResponse.Code);
        Assert.AreEqual(name, getFileInfoTaskResponse.Name);
    }

    [TestMethod]
    public async Task GetFileInfoTask_ReturnsFileInfoNotFoundException()
    {
        _fileDbContext
            .Setup<DbSet<FileInfo>>(c => c.FileInfo)
            .ReturnsDbSet(new List<FileInfo>());

        await Assert.ThrowsExceptionAsync<FileInfoNotFoundException>(() => _getFileInfoTask.GetAsync("testCode"));
    }
}