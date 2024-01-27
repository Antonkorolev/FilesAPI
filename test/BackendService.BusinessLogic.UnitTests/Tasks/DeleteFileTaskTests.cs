using BackendService.BusinessLogic.Tasks.DeleteFileTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileNotFoundException = BackendService.BusinessLogic.Exceptions.FileNotFoundException;

namespace BackendService.BusinessLogic.UnitTests.Tasks;

[TestClass]
public sealed class DeleteFileTaskTests : UnitTestsBase
{
    private IDeleteFileTask _deleteFileTask = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _deleteFileTask = new DeleteFileTask();
    }

    [TestMethod]
    public void DeleteFileTask__ExecuteSuccessfully()
    {
        var exception = Assert.ThrowsException<FileNotFoundException>(() => _deleteFileTask.Delete(Path1));

        Assert.AreEqual($"File not found. Current path: {Path1}", exception.Message);
    }
}