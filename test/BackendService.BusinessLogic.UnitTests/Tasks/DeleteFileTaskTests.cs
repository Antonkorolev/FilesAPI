using System.IO.Abstractions.TestingHelpers;
using BackendService.BusinessLogic.Tasks.DeleteFileTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileNotFoundException = BackendService.BusinessLogic.Exceptions.FileNotFoundException;

namespace BackendService.BusinessLogic.UnitTests.Tasks;

[TestClass]
public sealed class DeleteFileTaskTests
{
    private IDeleteFileTask _deleteFileTask;
    private MockFileSystem _fileSystem;

    [TestInitialize]
    public void TestInitialize()
    {
        _fileSystem = new MockFileSystem();
        _deleteFileTask = new DeleteFileTask(_fileSystem);
    }

    [TestMethod]
    public void DeleteFileTask_ExecuteSuccessfully()
    {
        var path = Path.Combine("repo", "test.txt");
        _fileSystem.AddFile(path, new MockFileData("test"));

        _deleteFileTask.Delete(path);

        Assert.IsFalse(_fileSystem.File.Exists(path));
    }

    [TestMethod]
    public void DeleteFileTask__ExecuteSuccessfully()
    {
        var path = Path.Combine("repo", "test.txt");

        var exception = Assert.ThrowsException<FileNotFoundException>(() => _deleteFileTask.Delete(path));

        Assert.AreEqual($"File not found. Current path: {path}", exception.Message);
    }
}