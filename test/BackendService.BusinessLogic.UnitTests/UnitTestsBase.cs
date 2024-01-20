using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BackendService.BusinessLogic.UnitTests;

public class UnitTestsBase
{
    protected const int FileInfoId = 1;
    protected const int FileChangeHistoryId = 1;
    protected const string DefaultFileCode = "testCode1";
    protected const string NewFileCode = "testCode2";
    protected const string ShortFileCode = "t";
    protected const string DefaultFileName = "testName1.txt";
    protected const string NewFileName = "testName2.txt";
    protected const string UserCode = "testUserCode";
    protected const string FileContent = "test content";

    protected static readonly DateTime CurrentDateTime = DateTime.UtcNow;
    protected static readonly string Path1 = Path.Combine("repo", DefaultFileCode[0].ToString(), DefaultFileCode[1].ToString(), DefaultFileName);
    protected static readonly string Path2 = Path.Combine("repo", NewFileCode[0].ToString(), NewFileCode[1].ToString(), NewFileName);

    protected FileDbContext CreateFileDbContext(string dbName)
    {
        return new FileDbContext(new DbContextOptionsBuilder<FileDbContext>()
            .UseInMemoryDatabase(dbName)
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);
    }
    
    protected static async Task<Stream> GetStreamAsync(string content)
    {
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();
        memoryStream.Position = 0;

        return memoryStream;
    }

    protected static async Task<string> GetFileContentAsync(Stream stream)
    {
        using var reader = new StreamReader(stream);
            
        return await reader.ReadToEndAsync();
    }

    protected static string GetFileNameFromPath(string path)
    {
        return Path.GetFileName(path);
    }
}