using System.IO.Abstractions.TestingHelpers;
using BackendService.BusinessLogic.Tasks.DeleteFileTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileNotFoundException = BackendService.BusinessLogic.Exceptions.FileNotFoundException;

namespace BackendService.BusinessLogic.UnitTests.Tasks;

[TestClass]
public sealed class DeleteFileTaskTests : UnitTestsBase
{
    private IDeleteFileTask _deleteFileTask = default!;
    private MockFileSystem _fileSystem = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _fileSystem = new MockFileSystem();
        _deleteFileTask = new DeleteFileTask(_fileSystem);
    }

    [TestMethod]
    public void DeleteFileTask_ExecuteSuccessfully()
    {
        _fileSystem.AddFile(Path1, new MockFileData("test"));

        _deleteFileTask.Delete(Path1);

        Assert.IsFalse(_fileSystem.File.Exists(Path1));
    }

    [TestMethod]
    public void DeleteFileTask__ExecuteSuccessfully()
    {
        var exception = Assert.ThrowsException<FileNotFoundException>(() => _deleteFileTask.Delete(Path1));

        Assert.AreEqual($"File not found. Current path: {Path1}", exception.Message);
    }
}