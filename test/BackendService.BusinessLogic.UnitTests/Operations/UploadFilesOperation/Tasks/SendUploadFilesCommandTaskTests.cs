using BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand;
using BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand.Models;
using Common;
using Common.UploadFiles;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NServiceBus.Callbacks.Testing;
using NServiceBus.Testing;

namespace BackendService.BusinessLogic.UnitTests.Operations.UploadFilesOperation.Tasks;

[TestClass]
public sealed class SendUploadFilesCommandTaskTests : UnitTestsBase
{
    private TestableCallbackAwareSession _testableCallbackAwareSession = default!;
    private Mock<ILogger<SendUploadFilesCommandTask>> _logger = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _testableCallbackAwareSession = new TestableCallbackAwareSession();
        _logger = new Mock<ILogger<SendUploadFilesCommandTask>>();
    }

    [TestMethod]
    public async Task SendUploadFilesCommandTask_ExecuteSuccessfully()
    {
        var command = new UploadFilesCommand(It.IsAny<IEnumerable<UploadFiles>>());
        var response = new FilesChangeResponseMessage(Status.OK, null);

        _testableCallbackAwareSession.When(
            matcher: (UploadFilesCommand message, SendOptions sendOptions) => message == command && sendOptions.GetHeaders().ContainsKey("Simulated.Header"),
            response: response);

        var sendOptions = new SendOptions();
        sendOptions.SetHeader("Simulated.Header", "value");

        var result = await _testableCallbackAwareSession.Request<FilesChangeResponseMessage>(command, sendOptions);

        Assert.AreEqual(response, result);
    }
    
    [TestMethod]
    public async Task SendUploadFilesCommandTask_ExecuteWithError()
    {
        var command = new UploadFilesCommand(It.IsAny<IEnumerable<UploadFiles>>());
        var response = new FilesChangeResponseMessage(Status.Error, "Test error");

        _testableCallbackAwareSession.When(
            matcher: (UploadFilesCommand message, SendOptions sendOptions) => message == command && sendOptions.GetHeaders().ContainsKey("Simulated.Header"),
            response: response);

        var sendOptions = new SendOptions();
        sendOptions.SetHeader("Simulated.Header", "value");

        var result = await _testableCallbackAwareSession.Request<FilesChangeResponseMessage>(command, sendOptions);

        Assert.AreEqual(response, result);
    }
}