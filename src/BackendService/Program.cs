using BackendService.Configuration;
using BackendService.Extensions;
using BackendService.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

configuration.AddJsonFile("appsettings.json");
configuration.AddJsonFile($"appsettings.{env}.json", optional: true);
configuration.AddEnvironmentVariables();

builder.Host.UseSerilog();
builder.Host.UseNServiceBus(b => NsbConfiguration.GetEndpointConfiguration(b.Configuration));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFileDbContext("FileDb", configuration);
builder.Services.AddUploadFileOperation();
builder.Services.AddUploadFilesOperation();
builder.Services.AddUpdateFileOperation();
builder.Services.AddGetFilesOperation();
builder.Services.AddGetFileOperation();
builder.Services.AddDeleteFileOperation();
builder.Services.AddCommonTasks(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.AddSwagger();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();