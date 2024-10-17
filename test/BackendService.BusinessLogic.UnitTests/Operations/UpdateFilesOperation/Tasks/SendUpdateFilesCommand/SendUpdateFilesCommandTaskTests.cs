using BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.SendUpdateFilesCommand;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NServiceBus.Callbacks.Testing;
using Common.UpdateFiles;

namespace BackendService.BusinessLogic.UnitTests.Operations.UpdateFilesOperation.Tasks.SendUpdateFilesCommand;

[TestClass]
public sealed class SendUpdateFilesCommandTaskTests : UnitTestsBase
{
    private TestableCallbackAwareSession _testableCallbackAwareSession = default!;
    private Mock<ILogger<SendUpdateFilesCommandTask>> _logger = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _testableCallbackAwareSession = new TestableCallbackAwareSession();
        _logger = new Mock<ILogger<SendUpdateFilesCommandTask>>();
    }
    
    [TestMethod]
    public async Task SendUpdateFilesCommandTask_ExecuteSuccessfully()
    {
        var command = new UpdateFilesCommand(It.IsAny<IEnumerable<UpdateFiles>>());
        var response = new FilesChangeResponseMessage(Status.OK, null);

        _testableCallbackAwareSession.When(
            matcher: (UpdateFilesCommand message, SendOptions sendOptions) => message == command && sendOptions.GetHeaders().ContainsKey("Simulated.Header"),
            response: response);

        var sendOptions = new SendOptions();
        sendOptions.SetHeader("Simulated.Header", "value");

        var result = await _testableCallbackAwareSession.Request<FilesChangeResponseMessage>(command, sendOptions);

        Assert.AreEqual(response, result);
    }
    
    [TestMethod]
    public async Task SendUploadFilesCommandTask_ExecuteWithError()
    {
        var command = new UpdateFilesCommand(It.IsAny<IEnumerable<UpdateFiles>>());
        var response = new FilesChangeResponseMessage(Status.Error, "Test error");

        _testableCallbackAwareSession.When(
            matcher: (UpdateFilesCommand message, SendOptions sendOptions) => message == command && sendOptions.GetHeaders().ContainsKey("Simulated.Header"),
            response: response);

        var sendOptions = new SendOptions();
        sendOptions.SetHeader("Simulated.Header", "value");

        var result = await _testableCallbackAwareSession.Request<FilesChangeResponseMessage>(command, sendOptions);

        Assert.AreEqual(response, result);
    }
}