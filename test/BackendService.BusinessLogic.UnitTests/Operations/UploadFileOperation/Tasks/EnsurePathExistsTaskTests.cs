using System.IO.Abstractions.TestingHelpers;
using BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.EnsurePathExistsTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests.Operations.UploadFileOperation.Tasks;

[TestClass]
public sealed class EnsurePathExistsTaskTests : UnitTestsBase
{
    private MockFileSystem _fileSystem = default!;
    private IEnsurePathExistsTask _ensurePathExistsTask = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _fileSystem = new MockFileSystem();
        _ensurePathExistsTask = new EnsurePathExistsTask(_fileSystem);
    }

    [TestMethod]
    public void EnsurePathExistsTask_DirectoryNotExists_ExecuteSuccessfully()
    {
        _ensurePathExistsTask.EnsureExisting(Path1);

        var dir = _fileSystem.Path.GetDirectoryName(Path1);

        Assert.IsTrue(_fileSystem.Directory.Exists(dir));
    }

    [TestMethod]
    public void EnsurePathExistsTask_DirectoryExists_ExecuteSuccessfully()
    {
        var dir = _fileSystem.Path.GetDirectoryName(Path1) ?? throw new Exception($"Directory name in Path = '{Path1}' not found");

        _fileSystem.Directory.CreateDirectory(dir);

        _ensurePathExistsTask.EnsureExisting(Path1);

        Assert.IsTrue(_fileSystem.Directory.Exists(dir));
    }
    
    [TestMethod]
    public void EnsurePathExistsTask_PathIsNull_ThrowsDirectoryNotFoundException()
    {
        var exception = Assert.ThrowsException<DirectoryNotFoundException>(() => _ensurePathExistsTask.EnsureExisting(null));

        Assert.AreEqual($"Can't get directory from path = ''", exception.Message);
    }
    
    [TestMethod]
    public void EnsurePathExistsTask_PathIsEmpty_ThrowsDirectoryNotFoundException()
    {
        var exception = Assert.ThrowsException<DirectoryNotFoundException>(() => _ensurePathExistsTask.EnsureExisting(string.Empty));

        Assert.AreEqual($"Can't get directory from path = ''", exception.Message);
    }
}