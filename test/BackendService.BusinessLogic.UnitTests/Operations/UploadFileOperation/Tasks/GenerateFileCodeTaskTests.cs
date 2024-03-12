using BackendService.BusinessLogic.Operations.UploadFile.Tasks.GenerateFileCodeTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests.Operations.UploadFileOperation.Tasks;

[TestClass]
public sealed class GenerateFileCodeTaskTests : UnitTestsBase
{
    private IGenerateFileCodeTask _generateFileCodeTask = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _generateFileCodeTask = new GenerateFileCodeTask();
    }

    [TestMethod]
    public async Task GenerateFileCodeTask_ExecuteSuccessfully()
    {
        var stream = await GetStreamAsync(DefaultFileContent).ConfigureAwait(false);

        var fileCode = await _generateFileCodeTask.GenerateAsync(stream).ConfigureAwait(false);

        Assert.IsNotNull(fileCode);
        Assert.AreNotEqual(string.Empty, fileCode);
    }
}