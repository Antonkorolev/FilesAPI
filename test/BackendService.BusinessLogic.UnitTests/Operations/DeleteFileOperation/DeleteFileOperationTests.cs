using BackendService.BusinessLogic.Exceptions;
using BackendService.BusinessLogic.Operations.DeleteFileOperation.Models;
using BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileInfoTask;
using BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileTask;
using BackendService.BusinessLogic.Tasks.AuthorizationTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask;
using BackendService.BusinessLogic.Tasks.GetFileInfoTask.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogicDeleteFileOperation = BackendService.BusinessLogic.Operations.DeleteFileOperation.DeleteFileOperation;
using FileNotFoundException = BackendService.BusinessLogic.Exceptions.FileNotFoundException;

namespace BackendService.BusinessLogic.UnitTests.Operations.DeleteFileOperation;

[TestClass]
public sealed class DeleteFileOperationTests
{
    private readonly Mock<IAuthorizationTask> _authorizationTask;
    private readonly Mock<IDeleteFileInfoTask> _deleteFileInfoTask;
    private readonly Mock<IDeleteFileTask> _deleteFileTask;
    private readonly Mock<IGetFileInfoTask> _getFileInfoTask;
    private readonly Mock<ILogger<BusinessLogicDeleteFileOperation>> _logger;
    private readonly BusinessLogicDeleteFileOperation _deleteFileOperation;

    public DeleteFileOperationTests()
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
        const int fileInfoId = 1;
        const string fileCode = "testFileCode";
        const string fileName = "testFileName";
        const string userCode = "testUserCode";

        _getFileInfoTask
            .Setup(u => u.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(() => new GetFileInfoTaskResponse(fileInfoId, fileCode, fileName));

        _deleteFileTask.Setup(d => d.Delete(It.IsAny<string>())).Verifiable();
        _deleteFileInfoTask.Setup(d => d.DeleteFileAsync(It.IsAny<int>(), CancellationToken.None)).Verifiable();

        await _deleteFileOperation.DeleteAsync(new DeleteFileOperationRequest(fileCode, userCode)).ConfigureAwait(false);
    }

    [TestMethod]
    public async Task DeleteFileOperation_FileInfoNotFound_ShouldThrowException()
    {
        const string fileCode = "testFileCode";
        const string userCode = "testUserCode";

        _getFileInfoTask
            .Setup(u => u.GetAsync(It.IsAny<string>()))
            .Throws(new FileInfoNotFoundException());

        var exception = await Assert.ThrowsExceptionAsync<FileInfoNotFoundException>(() => _deleteFileOperation.DeleteAsync(new DeleteFileOperationRequest(fileCode, userCode)));

        Assert.AreEqual("FileInfo not found in database", exception.Message);
    }

    [TestMethod]
    public async Task DeleteFileOperation_FileNotFound_ShouldThrowException()
    {
        const int fileInfoId = 1;
        const string fileCode = "testFileCode";
        const string fileName = "testFileName";
        const string userCode = "testUserCode";
        const string path = "testPath";

        _getFileInfoTask
            .Setup(u => u.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(() => new GetFileInfoTaskResponse(fileInfoId, fileCode, fileName));

        _deleteFileTask
            .Setup(d => d.Delete(It.IsAny<string>()))
            .Throws(new FileNotFoundException(path));

        var exception = await Assert.ThrowsExceptionAsync<FileNotFoundException>(() => _deleteFileOperation.DeleteAsync(new DeleteFileOperationRequest(fileCode, userCode)));

        Assert.AreEqual($"File not found. Current path: {path}", exception.Message);
    }
    
    [TestMethod]
    public async Task DeleteFileOperation_UserCodeLessThanTwoChar_ShouldThrowException()
    {
        const int fileInfoId = 1;
        const string fileCode = "t";
        const string fileName = "testFileName";
        const string userCode = "testUserCode";

        _getFileInfoTask
            .Setup(u => u.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(() => new GetFileInfoTaskResponse(fileInfoId, fileCode, fileName));

        var exception = await Assert.ThrowsExceptionAsync<FileCodeLengthException>(() => _deleteFileOperation.DeleteAsync(new DeleteFileOperationRequest(fileCode, userCode)));

        Assert.AreEqual($"FileCode length should 2 or more. Current value: {fileCode.Length}", exception.Message);
    }
}