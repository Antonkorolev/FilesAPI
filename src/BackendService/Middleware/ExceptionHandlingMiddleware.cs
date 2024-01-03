using Azure.Core;

namespace BackendService.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
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
        catch (IOException e)
        {
            _logger.LogError(e.Message);
        }
    }
}