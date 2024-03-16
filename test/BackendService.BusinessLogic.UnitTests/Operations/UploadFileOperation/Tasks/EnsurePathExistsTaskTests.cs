using BackendService.BusinessLogic.Operations.UploadFile.Tasks.EnsurePathExists;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests.Operations.UploadFileOperation.Tasks;

[TestClass]
public sealed class EnsurePathExistsTaskTests : UnitTestsBase
{
    private IEnsurePathExistsTask _ensurePathExistsTask = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _ensurePathExistsTask = new EnsurePathExistsTask();
    }

    [TestMethod]
    public void EnsurePathExistsTask_PathIsNull_ThrowsDirectoryNotFoundException()
    {
        var exception = Assert.ThrowsException<DirectoryNotFoundException>(() => _ensurePathExistsTask.EnsureExisting(null!));

        Assert.AreEqual($"Can't get directory from path = ''", exception.Message);
    }

    [TestMethod]
    public void EnsurePathExistsTask_PathIsEmpty_ThrowsDirectoryNotFoundException()
    {
        var exception = Assert.ThrowsException<DirectoryNotFoundException>(() => _ensurePathExistsTask.EnsureExisting(string.Empty));

        Assert.AreEqual($"Can't get directory from path = ''", exception.Message);
    }
}