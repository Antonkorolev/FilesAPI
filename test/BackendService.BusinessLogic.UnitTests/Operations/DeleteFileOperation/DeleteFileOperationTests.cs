using BackendService.BusinessLogic.Exceptions;
using BackendService.BusinessLogic.Operations.DeleteFileOperation.Models;
using BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileInfoTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using BackendService.BusinessLogic.Tasks.DeleteFileTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicDeleteFileOperation = BackendService.BusinessLogic.Operations.DeleteFileOperation.DeleteFileOperation;

namespace BackendService.BusinessLogic.UnitTests.Operations.DeleteFileOperation;

[TestClass]
public sealed class DeleteFileOperationTests : UnitTestsBase
{
    private Mock<IAuthorizationTask> _authorizationTask = default!;
    private Mock<IDeleteFileInfoTask> _deleteFileInfoTask = default!;
    private Mock<IDeleteFileTask> _deleteFileTask = default!;
    private Mock<IGetFileInfoTask> _getFileInfoTask = default!;
    private Mock<ILogger<BusinessLogicDeleteFileOperation>> _logger = default!;
    private BusinessLogicDeleteFileOperation _deleteFileOperation = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _authorizationTask = new Mock<IAuthorizationTask>();
        _deleteFileInfoTask = new Mock<IDeleteFileInfoTask>();
        _deleteFileTask = new Mock<IDeleteFileTask>();
        _getFileInfoTask = new Mock<IGetFileInfoTask>();
        _logger = new Mock<ILogger<BusinessLogicDeleteFileOperation>>();

        _deleteFileOperation = new BusinessLogicDeleteFileOperation(
            _authorizationTask.Object,
            _deleteFileInfoTask.Object,
            _deleteFileTask.Object,
            _getFileInfoTask.Object,
            _logger.Object);
    }

    [TestMethod]
    public async Task DeleteFileOperation_ExecuteSuccessfully()
    {
        _getFileInfoTask
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(() => new GetFileInfoTaskResponse(FileInfoId, DefaultFileCode, DefaultFileName));

        _deleteFileTask.Setup(d => d.Delete(It.IsAny<string>()));
        _deleteFileInfoTask.Setup(d => d.DeleteFileAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));

        await _deleteFileOperation.DeleteAsync(new DeleteFileOperationRequest(DefaultFileCode, DefaultUserCode)).ConfigureAwait(false);

        _authorizationTask.Verify(a => a.UserAuthorizationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _getFileInfoTask.Verify(d => d.GetAsync(It.IsAny<string>()), Times.Once);
        _deleteFileTask.Verify(d => d.Delete(It.IsAny<string>()), Times.Once);
        _deleteFileInfoTask.Verify(d => d.DeleteFileAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task DeleteFileOperation_UserCodeLessThanTwoChar_ShouldThrowException()
    {
        _getFileInfoTask
            .Setup(u => u.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(() => new GetFileInfoTaskResponse(FileInfoId, ShortFileCode, DefaultFileName));

        var exception = await Assert.ThrowsExceptionAsync<FileCodeLengthException>(() => _deleteFileOperation.DeleteAsync(new DeleteFileOperationRequest(DefaultFileCode, DefaultUserCode)));

        Assert.AreEqual($"FileCode length should 2 or more. Current value: {ShortFileCode.Length}", exception.Message);
    }
}