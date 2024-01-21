using System.IO.Abstractions.TestingHelpers;
using BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests.Operations.UpdateFileOperation.Tasks;

[TestClass]
public sealed class UpdateFileTaskTests : UnitTestsBase
{
    private MockFileSystem _fileSystem = default!;
    private IUpdateFileTask _updateFileTask = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _fileSystem = new MockFileSystem();
        _updateFileTask = new UpdateFileTask(_fileSystem);
    }

    [TestMethod]
    public async Task UpdateFileTask_ExecuteSuccessfully()
    {
        EnsureDirectoryCreated(_fileSystem);

        _fileSystem.AddFile(Path1, new MockFileData(DefaultFileContent));

        var stream = await GetStreamAsync(NewFileContent).ConfigureAwait(false);
        await _updateFileTask.UpdateAsync(stream, Path1, CancellationToken.None).ConfigureAwait(false);


        await using var fileStream = _fileSystem.File.Open(Path1, FileMode.Open, FileAccess.Read);

        var fileName = GetFileNameFromPath(fileStream.Name);

        Assert.AreEqual(DefaultFileName, fileName);

        var fileContent = await GetFileContentAsync(fileStream).ConfigureAwait(false);

        Assert.AreEqual(NewFileContent, fileContent);
    }

    [TestMethod]
    public async Task UpdateFileTask_StreamIsNull_Error()
    {
        EnsureDirectoryCreated(_fileSystem);

        await Assert.ThrowsExceptionAsync<NullReferenceException>(() => _updateFileTask.UpdateAsync(null, Path1, CancellationToken.None)).ConfigureAwait(false);
    }

    [TestMethod]
    public async Task UpdateFileTask_PathIsNull_Error()
    {
        var stream = await GetStreamAsync(NewFileContent).ConfigureAwait(false);

        await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _updateFileTask.UpdateAsync(stream, null, CancellationToken.None)).ConfigureAwait(false);
    }
}