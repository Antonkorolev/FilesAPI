namespace ProcessingService.BusinessLogic.Operations.UpdateFiles.Models;

public sealed class UpdateFilesOperationRequest(IEnumerable<UpdateFiles> updateFiles)
{
    public IEnumerable<UpdateFiles> UpdateFiles { get; set; } = updateFiles;
}