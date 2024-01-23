using System.Text;
using BackendService.BusinessLogic.Operations.DeleteFileOperation;
using BackendService.BusinessLogic.Operations.GetFileOperation;
using BackendService.BusinessLogic.Operations.GetFilesOperation;
using BackendService.BusinessLogic.Operations.UpdateFileOperation;
using BackendService.BusinessLogic.Operations.UploadFileOperation;
using BackendService.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendService.UnitTests.DependencyInjectionTests;

[TestClass]
public sealed class DependencyInjectionTests
{
    private ServiceProvider _serviceProvider = default!;

    private const string AppSettings = """ { "ConnectionStrings": { "FileDb": "*" } }""";

    [TestInitialize]
    public void TestInitialize()
    {
        var byteArray = Encoding.ASCII.GetBytes(AppSettings);
        var configurationStream = new MemoryStream(byteArray);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ILoggerFactory>(_ => NullLoggerFactory.Instance);
        serviceCollection.AddLogging();

        var configuration = new ConfigurationBuilder()
            .AddJsonStream(configurationStream)
            .Build();

        serviceCollection.AddFileDbContext("FileDb", configuration);
        serviceCollection.AddUploadFileOperation();
        serviceCollection.AddFileSystem();
        serviceCollection.AddUpdateFileOperation();
        serviceCollection.AddGetFilesOperation();
        serviceCollection.AddGetFileOperation();
        serviceCollection.AddDeleteFileOperation();
        serviceCollection.AddZipArchive();
        serviceCollection.AddCommonTasks();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [TestMethod]
    public void UploadFileOperation_ConfigureCorrectly()
    {
        var service = _serviceProvider.GetService<IUploadFileOperation>();

        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void UpdateFileOperation_ConfigureCorrectly()
    {
        var service = _serviceProvider.GetService<IUpdateFileOperation>();

        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetFilesOperation_ConfigureCorrectly()
    {
        var service = _serviceProvider.GetService<IGetFilesOperation>();

        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void GetFileOperation_ConfigureCorrectly()
    {
        var service = _serviceProvider.GetService<IGetFileOperation>();

        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void DeleteFileOperation_ConfigureCorrectly()
    {
        var service = _serviceProvider.GetService<IDeleteFileOperation>();

        Assert.IsNotNull(service);
    }
}