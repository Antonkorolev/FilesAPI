using BackendService.BusinessLogic.Operations.DeleteFile;
using BackendService.BusinessLogic.Operations.DeleteFile.Models;
using BackendService.BusinessLogic.Operations.GetFile;
using BackendService.BusinessLogic.Operations.GetFile.Models;
using BackendService.BusinessLogic.Operations.GetFiles;
using BackendService.BusinessLogic.Operations.GetFiles.Models;
using BackendService.BusinessLogic.Operations.UpdateFile;
using BackendService.BusinessLogic.Operations.UpdateFile.Models;
using BackendService.BusinessLogic.Operations.UploadFile;
using BackendService.BusinessLogic.Operations.UploadFile.Models;
using BackendService.Contracts.DeleteFile;
using BackendService.Contracts.GetFile;
using BackendService.Contracts.GetFiles;
using Microsoft.AspNetCore.Mvc;
using BackendService.Contracts.UpdateFile;
using BackendService.Contracts.UploadFile;

namespace BackendService.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IUploadFileOperation _uploadFileOperation;
    private readonly IUpdateFileOperation _updateFileOperation;
    private readonly IGetFilesOperation _getFilesOperation;
    private readonly IGetFileOperation _getFileOperation;
    private readonly IDeleteFileOperation _deleteFileOperation;

    public FileController(
        IUploadFileOperation uploadFileOperation,
        IUpdateFileOperation updateFileOperation,
        IGetFilesOperation getFilesOperation,
        IGetFileOperation getFileOperation,
        IDeleteFileOperation deleteFileOperation)
    {
        _uploadFileOperation = uploadFileOperation;
        _updateFileOperation = updateFileOperation;
        _getFilesOperation = getFilesOperation;
        _getFileOperation = getFileOperation;
        _deleteFileOperation = deleteFileOperation;
    }

    [HttpPost("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadFileAsync([FromForm] UploadFileRequest request)
    {
        var result = await _uploadFileOperation.UploadAsync(new UploadFileOperationRequest(request.File.OpenReadStream(), request.File.FileName, GetUserCode())).ConfigureAwait(false);

        return Ok(result);
    }

    [HttpPost("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateFileAsync([FromForm] UpdateFileRequest request)
    {
        await _updateFileOperation.UpdateAsync(new UpdateFileOperationRequest(request.FileCode, request.File.OpenReadStream(), request.File.FileName, GetUserCode())).ConfigureAwait(false);

        return Ok();
    }

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteFileAsync(DeleteFileRequest request)
    {
        await _deleteFileOperation.DeleteAsync(new DeleteFileOperationRequest(request.FileCode, GetUserCode())).ConfigureAwait(false);

        return Ok();
    }

    [HttpGet("get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFileAsync([FromQuery] GetFileRequest request)
    {
        var getFileOperationResponse = await _getFileOperation.GetFileAsync(new GetFileOperationRequest(request.FileCode, GetUserCode())).ConfigureAwait(false);

        return File(getFileOperationResponse.Stream, "application/octet-stream", getFileOperationResponse.FileName);
    }

    [HttpPost("getArray")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFilesAsync(GetFilesRequest request)
    {
        var byteArray = await _getFilesOperation.GetFilesAsync(new GetFilesOperationRequest(request.FileCodes, GetUserCode())).ConfigureAwait(false);

        return File(byteArray, "application/zip", "Files.zip");
    }

    private string GetUserCode()
    {
        return HttpContext.Request.Headers["UserCode"].ToString();
    }
}