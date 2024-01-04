using BackendService.Extensions;
using BackendService.Middleware;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
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
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFileDbContext("DefaultConnection", configuration);
builder.Services.AddUploadFileOperation();
builder.Services.AddUpdateFileOperation();
builder.Services.AddGetFilesOperation();
builder.Services.AddGetFileOperation();
builder.Services.AddDeleteFileOperation();
builder.Services.AddCommonTasks();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();