using System.Net;
using System.Runtime.CompilerServices;
using BackendService.BusinessLogic.Operations.DeleteFile;
using BackendService.BusinessLogic.Operations.DeleteFiles;
using BackendService.BusinessLogic.Operations.GetFile;
using BackendService.BusinessLogic.Operations.GetFile.Models;
using BackendService.BusinessLogic.Operations.GetFiles;
using BackendService.BusinessLogic.Operations.GetFiles.Models;
using BackendService.BusinessLogic.Operations.UpdateFile;
using BackendService.BusinessLogic.Operations.UpdateFile.Models;
using BackendService.BusinessLogic.Operations.UpdateFiles;
using BackendService.BusinessLogic.Operations.UploadFile;
using BackendService.BusinessLogic.Operations.UploadFile.Models;
using BackendService.BusinessLogic.Operations.UploadFiles;
using BackendService.BusinessLogic.Operations.UploadFiles.Models;
using BackendService.Contracts.DeleteFile;
using BackendService.Contracts.DeleteFiles;
using BackendService.Contracts.GetFile;
using BackendService.Contracts.GetFiles;
using BackendService.Contracts.UpdateFile;
using BackendService.Contracts.UpdateFiles;
using BackendService.Contracts.UploadFile;
using BackendService.Contracts.UploadFIles;
using BackendService.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BackendService.UnitTests.Controllers.FileControllerTests;

[TestClass]
public sealed class FileControllerTests
{
    private readonly FileController _fileController;
    private readonly Mock<IUploadFileOperation> _uploadFileOperation;
    private readonly Mock<IUploadFilesOperation> _uploadFilesOperation;
    private readonly Mock<IUpdateFileOperation> _updateFileOperation;
    private readonly Mock<IUpdateFilesOperation> _updateFilesOperation;
    private readonly Mock<IGetFilesOperation> _getFilesOperation;
    private readonly Mock<IGetFileOperation> _getFileOperation;
    private readonly Mock<IDeleteFileOperation> _deleteFileOperation;
    private readonly Mock<IDeleteFilesOperation> _deleteFilesOperation;

    public FileControllerTests()
    {
        _uploadFileOperation = new Mock<IUploadFileOperation>();
        _uploadFilesOperation = new Mock<IUploadFilesOperation>();
        _updateFileOperation = new Mock<IUpdateFileOperation>();
        _updateFilesOperation = new Mock<IUpdateFilesOperation>();
        _getFilesOperation = new Mock<IGetFilesOperation>();
        _getFileOperation = new Mock<IGetFileOperation>();
        _deleteFileOperation = new Mock<IDeleteFileOperation>();
        _deleteFilesOperation = new Mock<IDeleteFilesOperation>();

        _fileController = new FileController(
            _uploadFileOperation.Object,
            _uploadFilesOperation.Object,
            _updateFileOperation.Object,
            _updateFilesOperation.Object,
            _getFilesOperation.Object,
            _getFileOperation.Object,
            _deleteFileOperation.Object,
            _deleteFilesOperation.Object);

        _fileController.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        _fileController.ControllerContext.HttpContext.Request.Headers["UserCode"] = "TestUserCode";
    }

    [TestMethod]
    public async Task UploadFileOperation_ReturnsActionResult()
    {
        const string content = "test";
        const string fileName = "test.pdf";
        const string fileCode = "TestFileCode";

        var formFileMock = await GetFormFileMock(content, fileName).ConfigureAwait(false);

        _uploadFileOperation
            .Setup(u => u.UploadAsync(It.IsAny<UploadFileOperationRequest>()))
            .ReturnsAsync(fileCode);

        var response = await _fileController.UploadFileAsync(
                new UploadFileRequest
                {
                    File = formFileMock.Object
                })
            .ConfigureAwait(false);

        Assert.IsInstanceOfType(response, typeof(IActionResult));

        var result = response as OkObjectResult ?? throw new Exception("Cast Response to OkObjectResult return null");

        Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        Assert.AreEqual(fileCode, result.Value);
    }

    [TestMethod]
    public async Task UploadFilesOperation_ReturnsActionResult()
    {
        const string firstFileContent = "test1";
        const string firstFileName = "test2.pdf";

        const string secondFileContent = "test1";
        const string secondFileName = "test2.pdf";

        var fileCodes = new string[] { "TestFileCode1", "TestFileCode2" };

        var firstFormFileMock = await GetFormFileMock(firstFileContent, firstFileName).ConfigureAwait(false);
        var secondFormFileMock = await GetFormFileMock(secondFileContent, secondFileName).ConfigureAwait(false);

        _uploadFilesOperation
            .Setup(u => u.UploadAsync(It.IsAny<UploadFilesOperationRequest>()))
            .ReturnsAsync(fileCodes);

        var response = await _fileController.UploadFilesAsync(
                new UploadFilesRequest
                {
                    Files = new[] { firstFormFileMock.Object, secondFormFileMock.Object }
                })
            .ConfigureAwait(false);

        Assert.IsInstanceOfType(response, typeof(IActionResult));

        var result = response as OkObjectResult ?? throw new Exception("Cast Response to OkObjectResult return null");

        Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        Assert.AreEqual(fileCodes, result.Value);
    }

    [TestMethod]
    public async Task UpdateFileOperation_ReturnsActionResult()
    {
        const string content = "test";
        const string fileName = "test.pdf";
        const string fileCode = "TestFileCode";

        var formFileMock = await GetFormFileMock(content, fileName).ConfigureAwait(false);

        var response = await _fileController.UpdateFileAsync(
                new UpdateFileRequest
                {
                    File = formFileMock.Object,
                    FileCode = fileCode
                })
            .ConfigureAwait(false);

        Assert.IsInstanceOfType(response, typeof(IActionResult));

        var result = response as OkResult ?? throw new Exception("Cast Response to OkResult return null");

        Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
    }

    [TestMethod]
    public async Task UpdateFilesOperation_ReturnsActionResult()
    {
        const string firstFileContent = "test1";
        const string firstFileName = "test2.pdf";
        const string firstFileCode = "testCode1";

        const string secondFileContent = "test1";
        const string secondFileName = "test2.pdf";
        const string secondFileCode = "testCode2";

        var firstFormFileMock = await GetFormFileMock(firstFileContent, firstFileName).ConfigureAwait(false);
        var secondFormFileMock = await GetFormFileMock(secondFileContent, secondFileName).ConfigureAwait(false);

        var formFileCollectionMock = new Mock<FormFileCollection>();
        formFileCollectionMock.Object.Add(firstFormFileMock.Object);
        formFileCollectionMock.Object.Add(secondFormFileMock.Object);

        var response = await _fileController.UpdateFilesAsync(
                formFileCollectionMock.Object,
                new[] { firstFileCode, secondFileCode })
            .ConfigureAwait(false);

        Assert.IsInstanceOfType(response, typeof(IActionResult));

        var result = response as OkResult ?? throw new Exception("Cast Response to OkResult return null");

        Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
    }

    [TestMethod]
    public async Task GetFileOperation_ReturnsActionResult()
    {
        const string fileName = "test.pdf";
        const string content = "test";
        const string fileCode = "TestFileCode";

        var stream = await GetStreamAsync(content).ConfigureAwait(false);

        _getFileOperation
            .Setup(u => u.GetFileAsync(It.IsAny<GetFileOperationRequest>()))
            .ReturnsAsync(new GetFileOperationResponse(fileName, stream));

        var response = await _fileController.GetFileAsync(
                new GetFileRequest
                {
                    FileCode = fileCode
                })
            .ConfigureAwait(false);

        Assert.IsInstanceOfType(response, typeof(IActionResult));

        var result = response as FileStreamResult ?? throw new Exception("Cast Response to FileStreamResult return null");

        Assert.AreEqual(fileName, result.FileDownloadName);
        Assert.AreEqual("application/octet-stream", result.ContentType);
        Assert.AreEqual(4, result.FileStream.Length);
    }

    [TestMethod]
    public async Task GetFilesOperation_ReturnsActionResult()
    {
        const string fileName = "Files.zip";
        const string content = "test";
        const string fileCode = "TestFileCode";

        var stream = await GetStreamAsync(content).ConfigureAwait(false);

        _getFilesOperation
            .Setup(u => u.GetFilesAsync(It.IsAny<GetFilesOperationRequest>()))
            .ReturnsAsync(((MemoryStream)stream).ToArray());

        var response = await _fileController.GetFilesAsync(
                new GetFilesRequest
                {
                    FileCodes = new[] { fileCode }
                })
            .ConfigureAwait(false);

        Assert.IsInstanceOfType(response, typeof(IActionResult));

        var result = Get<FileContentResult>(response);

        Assert.AreEqual(fileName, result.FileDownloadName);
        Assert.AreEqual("application/zip", result.ContentType);
        Assert.AreEqual(4, result.FileContents.Length);
    }

    [TestMethod]
    public async Task DeleteFileOperation_ReturnsActionResult()
    {
        const string fileCode = "TestFileCode";

        var response = await _fileController.DeleteFileAsync(
                new DeleteFileRequest
                {
                    FileCode = fileCode
                })
            .ConfigureAwait(false);

        Assert.IsInstanceOfType(response, typeof(IActionResult));

        var result = Get<OkResult>(response);

        Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
    }

    [TestMethod]
    public async Task DeleteFilesOperation_ReturnsActionResult()
    {
        var fileCodes = new[] { "TestFileCode1", "TestFileCode2" };

        var response = await _fileController.DeleteFilesAsync(
                new DeleteFilesRequest
                {
                    FileCodes = fileCodes
                })
            .ConfigureAwait(false);

        Assert.IsInstanceOfType(response, typeof(IActionResult));

        var result = Get<OkResult>(response);

        Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
    }

    private static async Task<Stream> GetStreamAsync(string content)
    {
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();

        return memoryStream;
    }

    private static async Task<Mock<IFormFile>> GetFormFileMock(string content, string fileName)
    {
        var fileMock = new Mock<IFormFile>();

        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();

        fileMock.Setup(f => f.OpenReadStream()).Returns(memoryStream);
        fileMock.Setup(f => f.FileName).Returns(fileName);
        fileMock.Setup(f => f.Length).Returns(memoryStream.Length);

        return fileMock;
    }

    private static T Get<T>(IActionResult actionResult) where T : class
    {
        return actionResult as T ?? throw new Exception($"Cast Response to {nameof(T)} return null");
    }
}