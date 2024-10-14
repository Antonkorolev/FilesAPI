using BackendService.BusinessLogic.Operations.DeleteFile.Models;
using BackendService.BusinessLogic.Operations.DeleteFile.Tasks.DeleteFileInfo;
using BackendService.BusinessLogic.Tasks.Authorization;
using BackendService.BusinessLogic.Tasks.DeleteFile;
using BackendService.BusinessLogic.Tasks.GetFileInfo;
using BackendService.BusinessLogic.Tasks.GetFileInfo.Models;
using BackendService.BusinessLogic.Tasks.PathBuilder;
using BackendService.BusinessLogic.Tasks.SendNotificationCommand;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicDeleteFileOperation = BackendService.BusinessLogic.Operations.DeleteFile.DeleteFileOperation;

namespace BackendService.BusinessLogic.UnitTests.Operations.DeleteFile;

[TestClass]
public sealed class DeleteFileOperationTests : UnitTestsBase
{
    private Mock<IAuthorizationTask> _authorizationTask = default!;
    private Mock<IDeleteFileInfoTask> _deleteFileInfoTask = default!;
    private Mock<IDeleteFileTask> _deleteFileTask = default!;
    private Mock<IGetFileInfoTask> _getFileInfoTask = default!;
    private Mock<ISendNotificationCommandTask> _sendUpdateFilesCommandTask = default!;
    private Mock<IPathBuilderTask> _pathBuilderTask = default!;
    private Mock<ILogger<BusinessLogicDeleteFileOperation>> _logger = default!;
    private BusinessLogicDeleteFileOperation _deleteFileOperation = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _authorizationTask = new Mock<IAuthorizationTask>();
        _deleteFileInfoTask = new Mock<IDeleteFileInfoTask>();
        _deleteFileTask = new Mock<IDeleteFileTask>();
        _getFileInfoTask = new Mock<IGetFileInfoTask>();
        _pathBuilderTask = new Mock<IPathBuilderTask>();
        _sendUpdateFilesCommandTask = new Mock<ISendNotificationCommandTask>();
        _logger = new Mock<ILogger<BusinessLogicDeleteFileOperation>>();

        _deleteFileOperation = new BusinessLogicDeleteFileOperation(
            _authorizationTask.Object,
            _deleteFileInfoTask.Object,
            _deleteFileTask.Object,
            _getFileInfoTask.Object,
            _sendUpdateFilesCommandTask.Object,
            _logger.Object,
            _pathBuilderTask.Object);
    }

    [TestMethod]
    public async Task DeleteFileOperation_ExecuteSuccessfully()
    {
        _getFileInfoTask
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(() => new GetFileInfoTaskResponse(DefaultFileInfoId, DefaultFileCode, DefaultFileName));

        _deleteFileTask.Setup(d => d.DeleteAsync(It.IsAny<string>()));
        _deleteFileInfoTask.Setup(d => d.DeleteFileAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));

        await _deleteFileOperation.DeleteAsync(new DeleteFileOperationRequest(DefaultFileCode, DefaultUserCode)).ConfigureAwait(false);

        _authorizationTask.Verify(a => a.UserAuthorizationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _getFileInfoTask.Verify(d => d.GetAsync(It.IsAny<string>()), Times.Once);
        _deleteFileTask.Verify(d => d.DeleteAsync(It.IsAny<string>()), Times.Once);
        _deleteFileInfoTask.Verify(d => d.DeleteFileAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}