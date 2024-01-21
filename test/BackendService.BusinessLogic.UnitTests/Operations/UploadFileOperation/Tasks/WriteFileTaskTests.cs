using System.IO.Abstractions.TestingHelpers;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.WriteFileTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BackendService.BusinessLogic.UnitTests.Operations.UploadFileOperation.Tasks;

[TestClass]
public sealed class WriteFileTaskTests : UnitTestsBase
{
    private IWriteFileTask _writeFileTask = default!;
    private MockFileSystem _fileSystem = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _fileSystem = new MockFileSystem();
        _writeFileTask = new WriteFileTask(_fileSystem);
    }

    [TestMethod]
    public async Task WriteFileTask_ExecuteSuccessfully()
    {
        EnsureDirectoryCreated(_fileSystem);

        var steam = await GetStreamAsync(DefaultFileContent).ConfigureAwait(false);

        await _writeFileTask.WriteAsync(steam, Path1, It.IsAny<CancellationToken>()).ConfigureAwait(false);

        await using var fileStream = _fileSystem.File.Open(Path1, FileMode.Open, FileAccess.Read);

        var fileName = GetFileNameFromPath(fileStream.Name);

        Assert.AreEqual(DefaultFileName, fileName);

        var fileContent = await GetFileContentAsync(fileStream).ConfigureAwait(false);

        Assert.AreEqual(DefaultFileContent, fileContent);
    }
}