namespace IntegrationTests;

[TestClass]
public class Tests : IntegrationTestsBase
{
    [TestMethod]
    public async Task Test()
    {
        var client = GetClient();

        var response = await client.GetAsync("get?FileCode=8dfc2515-2129-4b0d-84ba-526d82222d38").ConfigureAwait(false);
        Console.WriteLine(response.StatusCode);
    }
}