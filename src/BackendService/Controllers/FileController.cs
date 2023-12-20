using Microsoft.AspNetCore.Mvc;
using BackendService.Contracts.UpdateFile;
using BackendService.Contracts.UploadFile;

namespace BackendService.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFileAsync([FromForm]UploadFileRequest request)
    {

        return Ok($"{request.FileCode}");
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateFileAsync([FromForm]UpdateFileRequest request)
    {

        return Ok();
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteFileAsync(string fileCode)
    {

        return Ok();
    }
    
    
    [HttpGet("get")]
    public async Task<IActionResult> GetFileAsync(string fileCode)
    {

        return Ok();
    }
}
