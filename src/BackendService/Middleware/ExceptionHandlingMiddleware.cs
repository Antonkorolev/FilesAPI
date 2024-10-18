using BackendService.BusinessLogic.Exceptions;
using FileNotFoundException = BackendService.BusinessLogic.Exceptions.FileNotFoundException;

namespace BackendService.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception _) when (_ is OperationCanceledException or TaskCanceledException)
        {
            _logger.LogInformation("Task canceled");
        }
        catch (FileInfoNotFoundException e)
        {
            _logger.LogInformation("FileInfo not found");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("Information about file not found").ConfigureAwait(false);
        }
        catch (FileCodeLengthException e)
        {
            _logger.LogInformation("FileCode is invalid");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("FileCode is invalid").ConfigureAwait(false);
        }
        catch (FileInfosCountException e)
        {
            _logger.LogInformation("FileInfo not found for some files");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Information about some files not found").ConfigureAwait(false);
        }
        catch (FileNotFoundException e)
        {
            _logger.LogInformation("File not found");
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("File not found").ConfigureAwait(false);
        }
        
        catch (IOException e)
        {
            _logger.LogError(e.Message);
        }
    }
}