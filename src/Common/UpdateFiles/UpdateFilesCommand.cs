namespace Common.UpdateFiles;

public sealed class UpdateFilesCommand(IEnumerable<UpdateFiles> updateFiles)
{
    public IEnumerable<UpdateFiles> UpdateFiles { get; set; } = updateFiles;
}