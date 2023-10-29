using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    [HttpPost("/upload")]
    public IActionResult UploadFile()
    {

        return Ok();
    }

    [HttpPost("/update")]
    public IActionResult UpdateFile()
    {

        return Ok();
    }
    
    [HttpDelete("/delete")]
    public IActionResult DeleteFile()
    {

        return Ok();
    }
    
    
    [HttpGet("/get")]
    public IActionResult GetFile()
    {

        return Ok();
    }
}
