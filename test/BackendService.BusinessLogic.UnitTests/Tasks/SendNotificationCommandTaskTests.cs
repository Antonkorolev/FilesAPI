using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NServiceBus.Testing;

namespace BackendService.BusinessLogic.UnitTests.Tasks;

[TestClass]
public sealed class SendNotificationCommandTaskTests : UnitTestsBase
{
    private TestableMessageSession _testableMessageSession = default!;
    private Mock<ILogger<SendNotificationCommandTask>> _logger = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _testableMessageSession = new TestableMessageSession();
        _logger = new Mock<ILogger<SendNotificationCommandTask>>();
    }

    [TestMethod]
    public async Task SendNotificationCommandTask_ThrowsDirectoryNotFoundException()
    {
        var command = new NotificationCommand
        {
            UpdateFileType = UpdateFileType.GetFile,
            FilesNames = FileCodes
        };

        var sendOptions = new SendOptions();
        sendOptions.SetHeader("Simulated.Header", "value");

        await _testableMessageSession.Send(command).ConfigureAwait(false);

        Assert.AreEqual(_testableMessageSession.SentMessages.Length, 1);
        Assert.AreEqual(_testableMessageSession.SentMessages[0].Message, command);
    }
}