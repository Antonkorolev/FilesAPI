using BackendService.BusinessLogic.Operations.UpdateFiles.Models;
using BackendService.BusinessLogic.Operations.UpdateFiles.Tasks;
using BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.SendUpdateFilesCommand;
using BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.SendUpdateFilesCommand.Models;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.EnsurePathExists;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;
using BackendService.BusinessLogic.Tasks.WriteFile;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicUpdateFilesOperation = BackendService.BusinessLogic.Operations.UpdateFiles.UpdateFilesOperation;

namespace BackendService.BusinessLogic.UnitTests.Operations.UpdateFilesOperation;

[TestClass]
public sealed class UpdateFilesOperationTests : UnitTestsBase
{
    private Mock<IAuthorizationTask> _authorizationTask = default!;
    private Mock<IEnsurePathExistsTask> _ensurePathExistsTask = default!;
    private Mock<IWriteFileTask> _writeFileTask = default!;
    private Mock<ISendNotificationCommandTask> _sendNotificationCommandTask = default!;
    private Mock<IPathBuilderTask> _pathBuilderTask = default!;
    private Mock<ISendUpdateFilesCommandTask> _sendUpdateFilesCommandTask = default!;
    private Mock<ILogger<BusinessLogicUpdateFilesOperation>> _logger = default!;
    private BusinessLogicUpdateFilesOperation _updateFilesOperation = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _authorizationTask = new Mock<IAuthorizationTask>();
        _ensurePathExistsTask = new Mock<IEnsurePathExistsTask>();
        _writeFileTask = new Mock<IWriteFileTask>();
        _sendNotificationCommandTask = new Mock<ISendNotificationCommandTask>();
        _pathBuilderTask = new Mock<IPathBuilderTask>();
        _sendUpdateFilesCommandTask = new Mock<ISendUpdateFilesCommandTask>();
        _logger = new Mock<ILogger<BusinessLogicUpdateFilesOperation>>();

        _updateFilesOperation = new BusinessLogicUpdateFilesOperation(
            _authorizationTask.Object,
            _ensurePathExistsTask.Object,
            _writeFileTask.Object,
            _sendNotificationCommandTask.Object,
            _pathBuilderTask.Object,
            _sendUpdateFilesCommandTask.Object,
            _logger.Object);
    }

    [TestMethod]
    public async Task UpdateFilesOperation_ExecuteSuccessfully()
    {
        var uploadFilesOperationRequest = new UpdateFilesOperationRequest(
            new[] { new UpdateFileData(It.IsAny<Stream>(), DefaultFileName, DefaultFileCode), new UpdateFileData(It.IsAny<Stream>(), NewFileName, NewFileCode) },
            DefaultUserCode);

        _ensurePathExistsTask.Setup(e => e.EnsureExistingAsync((It.IsAny<string>())));
        _writeFileTask.Setup(w => w.WriteAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        _sendUpdateFilesCommandTask.Setup(s => s.SendAsync(new SendUpdateFilesCommandTaskRequest(new List<SendUpdateFilesData>())));
        _sendNotificationCommandTask.Setup(s => s.SendAsync(new SendNotificationCommandTaskRequest(It.IsAny<UpdateFileType>(), It.IsAny<IEnumerable<string>>())));

        await _updateFilesOperation.UpdateAsync(uploadFilesOperationRequest).ConfigureAwait(false);

        _authorizationTask.Verify(a => a.UserAuthorizationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _pathBuilderTask.Verify(a => a.BuildAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        _ensurePathExistsTask.Verify(e => e.EnsureExistingAsync(It.IsAny<string>()), Times.Exactly(2));
        _writeFileTask.Verify(w => w.WriteAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        _sendUpdateFilesCommandTask.Verify(s => s.SendAsync(It.IsAny<SendUpdateFilesCommandTaskRequest>()), Times.Once);
        _sendNotificationCommandTask.Verify(s => s.SendAsync(It.IsAny<SendNotificationCommandTaskRequest>()), Times.Once);
    }
}