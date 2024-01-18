using DatabaseContext.FileDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BackendService.BusinessLogic.UnitTests;

public class UnitTestsBase
{
    protected const int FileInfoId = 1;
    protected const int FileChangeHistoryId = 1;
    protected const string FileCode1 = "testCode1";
    protected const string FileCode2 = "testCode2";
    protected const string ShortFileCode = "t";
    protected const string FileName1 = "testName1";
    protected const string FileName2 = "testName2";
    protected const string UserCode = "testUserCode";

    protected static readonly DateTime CurrentDateTime = DateTime.UtcNow;
    protected static string Path1 = Path.Combine("repo", FileCode1[0].ToString(), FileCode1[1].ToString(), FileName1);
    protected static string Path2 = Path.Combine("repo", FileCode2[0].ToString(), FileCode2[1].ToString(), FileName2);

    protected FileDbContext CreateFileDbContext(string dbName)
    {
        return new FileDbContext(new DbContextOptionsBuilder<FileDbContext>()
            .UseInMemoryDatabase(dbName)
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);
    }
}