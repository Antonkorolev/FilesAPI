using BackendService.BusinessLogic.Exceptions;
using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFileInfosTask;
using DatabaseContext.FileDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileInfo = DatabaseContext.FileDb.Models.FileInfo;

namespace BackendService.BusinessLogic.UnitTests.Operations.GetFilesOperation.Tasks;

[TestClass]
public sealed class GetFileInfosTaskTests : UnitTestsBase
{
    private IFileDbContext _context = default!;
    private GetFileInfosTask _getFileInfosTask = default!;


    [TestInitialize]
    public void TestInitialize()
    {
        _context = CreateFileDbContext();
        _getFileInfosTask = new GetFileInfosTask(_context);
    }

    [TestMethod]
    public async Task GetFileInfosTask_GetTwoFileInfos()
    {
        await _context.FileInfo.AddRangeAsync(
                new[]
                {
                    new FileInfo
                    {
                        Code = DefaultFileCode,
                        Name = DefaultFileName
                    },
                    new FileInfo
                    {
                        Code = NewFileCode,
                        Name = NewFileName
                    }
                })
            .ConfigureAwait(false);
        await _context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

        var getFileInfosTaskResponse = await _getFileInfosTask.GetAsync(new[] { DefaultFileCode, NewFileCode }).ConfigureAwait(false);

        Assert.AreEqual(2, getFileInfosTaskResponse.FileInfos.Count());

        var firstFileInfo = getFileInfosTaskResponse.FileInfos.First();

        Assert.AreEqual(DefaultFileInfoId, firstFileInfo.FileInfoId);
        Assert.AreEqual(DefaultFileCode, firstFileInfo.Code);
        Assert.AreEqual(DefaultFileName, firstFileInfo.Name);

        var secondFileInfo = getFileInfosTaskResponse.FileInfos.Last();

        Assert.AreEqual(NewFileInfoId, secondFileInfo.FileInfoId);
        Assert.AreEqual(NewFileCode, secondFileInfo.Code);
        Assert.AreEqual(NewFileName, secondFileInfo.Name);
    }

    [TestMethod]
    public async Task GetFileInfosTask_NotFoundAllFiles_ThrowException()
    {
        await _context.FileInfo.AddRangeAsync(
                new[]
                {
                    new FileInfo
                    {
                        Code = DefaultFileCode,
                        Name = DefaultFileName
                    }
                })
            .ConfigureAwait(false);
        await _context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

        var exception = await Assert.ThrowsExceptionAsync<FileInfosCountException>(() => _getFileInfosTask.GetAsync(new[] { DefaultFileCode, NewFileCode })).ConfigureAwait(false);

        Assert.AreEqual($"FileInfo count should be equal 2. Current value: 1", exception.Message);
    }
}