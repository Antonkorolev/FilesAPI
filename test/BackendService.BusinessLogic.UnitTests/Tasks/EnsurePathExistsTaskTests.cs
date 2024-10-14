using BackendService.BusinessLogic.Tasks.EnsurePathExists;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests.Tasks;

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
    public async Task EnsurePathExistsTask_PathIsNull_ThrowsDirectoryNotFoundException()
    {
        var exception = await Assert.ThrowsExceptionAsync<DirectoryNotFoundException>(() => _ensurePathExistsTask.EnsureExistingAsync(null!));

        Assert.AreEqual($"Can't get directory from path = ''", exception.Message);
    }

    [TestMethod]
    public async Task EnsurePathExistsTask_PathIsEmpty_ThrowsDirectoryNotFoundException()
    {
        var exception = await Assert.ThrowsExceptionAsync<DirectoryNotFoundException>(() => _ensurePathExistsTask.EnsureExistingAsync(string.Empty));

        Assert.AreEqual($"Can't get directory from path = ''", exception.Message);
    }
}