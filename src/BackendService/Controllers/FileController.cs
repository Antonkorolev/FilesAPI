using BackendService.BusinessLogic.Operations.DeleteFile;
using BackendService.BusinessLogic.Operations.DeleteFile.Models;
using BackendService.BusinessLogic.Operations.DeleteFiles;
using BackendService.BusinessLogic.Operations.DeleteFiles.Models;
using BackendService.BusinessLogic.Operations.GetFile;
using BackendService.BusinessLogic.Operations.GetFile.Models;
using BackendService.BusinessLogic.Operations.GetFiles;
using BackendService.BusinessLogic.Operations.GetFiles.Models;
using BackendService.BusinessLogic.Operations.UpdateFile;
using BackendService.BusinessLogic.Operations.UpdateFile.Models;
using BackendService.BusinessLogic.Operations.UpdateFiles;
using BackendService.BusinessLogic.Operations.UpdateFiles.Models;
using BackendService.BusinessLogic.Operations.UploadFile;
using BackendService.BusinessLogic.Operations.UploadFile.Models;
using BackendService.BusinessLogic.Operations.UploadFiles;
using BackendService.BusinessLogic.Operations.UploadFiles.Models;
using BackendService.Contracts.DeleteFile;
using BackendService.Contracts.DeleteFiles;
using BackendService.Contracts.GetFile;
using BackendService.Contracts.GetFiles;
using BackendService.Contracts.UpdateFile;
using BackendService.Contracts.UploadFile;
using BackendService.Contracts.UploadFIles;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IUploadFileOperation _uploadFileOperation;
    private readonly IUploadFilesOperation _uploadFilesOperation;
    private readonly IUpdateFileOperation _updateFileOperation;
    private readonly IUpdateFilesOperation _updateFilesOperation;
    private readonly IGetFilesOperation _getFilesOperation;
    private readonly IGetFileOperation _getFileOperation;
    private readonly IDeleteFileOperation _deleteFileOperation;
    private readonly IDeleteFilesOperation _deleteFilesOperation;

    public FileController(
        IUploadFileOperation uploadFileOperation,
        IUploadFilesOperation uploadFilesOperation,
        IUpdateFileOperation updateFileOperation,
        IUpdateFilesOperation updateFilesOperation,
        IGetFilesOperation getFilesOperation,
        IGetFileOperation getFileOperation,
        IDeleteFileOperation deleteFileOperation,
        IDeleteFilesOperation deleteFilesOperation)
    {
        _uploadFileOperation = uploadFileOperation;
        _uploadFilesOperation = uploadFilesOperation;
        _updateFileOperation = updateFileOperation;
        _updateFilesOperation = updateFilesOperation;
        _getFilesOperation = getFilesOperation;
        _getFileOperation = getFileOperation;
        _deleteFileOperation = deleteFileOperation;
        _deleteFilesOperation = deleteFilesOperation;
    }

    [HttpPost("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadFileAsync([FromForm] UploadFileRequest request)
    {
        var result = await _uploadFileOperation.UploadAsync(new UploadFileOperationRequest(request.File.OpenReadStream(), request.File.FileName, GetUserCode())).ConfigureAwait(false);

        return Ok(result);
    }

    [HttpPost("uploadArray")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadFilesAsync([FromForm] UploadFilesRequest request)
    {
        var getUploadFilesOperationRequest = GetUploadFilesOperationRequest(request);

        var result = await _uploadFilesOperation.UploadAsync(getUploadFilesOperationRequest).ConfigureAwait(false);

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

    [HttpPost("updateArray")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateFilesAsync([FromForm] IFormFileCollection fileCollection, [FromForm] IEnumerable<string> fileCodes)
    {
        var getUpdateFilesOperationRequest = GetUpdateFilesOperationRequest(fileCollection, fileCodes);

        await _updateFilesOperation.UpdateAsync(getUpdateFilesOperationRequest).ConfigureAwait(false);

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

    // TODO подумать над вынесением методов для работы с массивом файлов в отдельный контроллер
    [HttpDelete("deleteArray")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteFilesAsync(DeleteFilesRequest request)
    {
        await _deleteFilesOperation.DeleteAsync(new DeleteFilesOperationRequest(request.FileCodes, GetUserCode())).ConfigureAwait(false);

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

    private UploadFilesOperationRequest GetUploadFilesOperationRequest(UploadFilesRequest request)
    {
        IList<UploadFileData> fileData = request.Files.Select(file => new UploadFileData(file.OpenReadStream(), file.FileName)).ToList();

        fileData = fileData ?? throw new InvalidOperationException();

        return new UploadFilesOperationRequest(fileData, GetUserCode());
    }

    private UpdateFilesOperationRequest GetUpdateFilesOperationRequest(IFormFileCollection fileCollection, IEnumerable<string> fileCodes)
    {
        var enumerable = fileCodes.ToList();

        if (fileCollection.Count != enumerable.Count())
            throw new Exception("FileCodes count not equals FileCollection count");

        var fileData = fileCollection.Zip(enumerable).Select(file => new UpdateFileData(file.First.OpenReadStream(), file.First.FileName, file.Second)).ToList();

        fileData = fileData ?? throw new InvalidOperationException();

        return new UpdateFilesOperationRequest(fileData, GetUserCode());
    }
}