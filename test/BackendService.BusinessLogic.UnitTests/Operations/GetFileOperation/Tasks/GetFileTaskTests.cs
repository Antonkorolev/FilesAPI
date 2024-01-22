using System.IO.Abstractions.TestingHelpers;
using BackendService.BusinessLogic.Operations.GetFileOperation.Tasks.GetFileTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests.Operations.GetFileOperation.Tasks;

[TestClass]
public sealed class GetFileTaskTests : UnitTestsBase
{
    private MockFileSystem _fileSystem = default!;
    private IGetFileTask _getFileTask = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _fileSystem = new MockFileSystem();
        _getFileTask = new GetFileTask(_fileSystem);
    }

    [TestMethod]
    public async Task GetFileTask_ExecuteSuccessfully()
    {
        _fileSystem.AddFile(Path1, new MockFileData(DefaultFileContent));

        var stream = await _getFileTask.GetAsync(Path1).ConfigureAwait(false);

        var fileContent = await GetFileContentAsync(stream).ConfigureAwait(false);

        Assert.AreEqual(DefaultFileContent, fileContent);
    }

    [TestMethod]
    public async Task GetFileTask_FileNotExists_ShouldFail()
    {
        var exception = await Assert.ThrowsExceptionAsync<FileNotFoundException>(() => _getFileTask.GetAsync(Path2)).ConfigureAwait(false);

        Assert.AreEqual($"Could not find file '{Path2}'.", exception.Message);
    }

    [TestMethod]
    public async Task GetFileTask_PathIsNull_ShouldFail()
    {
        _fileSystem.AddFile(Path1, new MockFileData(DefaultFileContent));

        var exception = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _getFileTask.GetAsync(null)).ConfigureAwait(false);

        Assert.AreEqual("Value cannot be null. (Parameter 'path')", exception.Message);
    }

    [TestMethod]
    public async Task GetFileTask_PathIsEmpty_ShouldFail()
    {
        _fileSystem.AddFile(Path1, new MockFileData(DefaultFileContent));

        var exception = await Assert.ThrowsExceptionAsync<ArgumentException>(() => _getFileTask.GetAsync(string.Empty)).ConfigureAwait(false);

        Assert.AreEqual("Empty file name is not legal. (Parameter 'path')", exception.Message);
    }
}