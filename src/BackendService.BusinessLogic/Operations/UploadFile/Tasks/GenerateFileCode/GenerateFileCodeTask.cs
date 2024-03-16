using System.Security.Cryptography;

namespace BackendService.BusinessLogic.Operations.UploadFile.Tasks.GenerateFileCode;

public sealed class GenerateFileCodeTask : IGenerateFileCodeTask
{
    public async Task<string> GenerateAsync(Stream stream)
    {
        using var sha = SHA256.Create();
        var checksum = await sha.ComputeHashAsync(stream).ConfigureAwait(false);
        return BitConverter.ToString(checksum).Replace("-", string.Empty);
    }
}