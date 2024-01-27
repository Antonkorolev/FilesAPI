using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.BusinessLogic.UnitTests;

public class UnitTestsBase
{
    public TestContext TestContext { get; set; } = default!;

    protected const int DefaultFileInfoId = 1;
    protected const int NewFileInfoId = 2;
    protected const int FileChangeHistoryId = 1;
    protected const string DefaultFileCode = "testCode1";
    protected const string NewFileCode = "testCode2";
    protected const string ShortFileCode = "t";
    protected const string DefaultFileName = "testName1.txt";
    protected const string NewFileName = "testName2.txt";
    protected const string DefaultUserCode = "testUserCode1";
    protected const string NewUserCode = "testUserCode2";
    protected const string DefaultFileContent = "test content2";
    protected const string NewFileContent = "test context1";
    protected static readonly IEnumerable<string> FileCodes = new[] { "fileCode1", "fileCode2", "fileCode3" };

    protected static readonly DateTime CurrentDateTime = DateTime.UtcNow;
    protected static readonly string Path1 = Path.Combine("repo", DefaultFileCode[0].ToString(), DefaultFileCode[1].ToString(), DefaultFileName);
    protected static readonly string Path2 = Path.Combine("repo", NewFileCode[0].ToString(), NewFileCode[1].ToString(), NewFileName);

    protected FileDbContext CreateFileDbContext()
    {
        return new FileDbContext(new DbContextOptionsBuilder<FileDbContext>()
            .UseInMemoryDatabase($"{TestContext.TestName}Db")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);
    }

    protected static async Task<Stream> GetStreamAsync(string content)
    {
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();

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