namespace Common.UpdateFiles;

public sealed class UpdateFilesCommand(IEnumerable<UpdateFiles> updateFiles) : ICommand
{
    public IEnumerable<UpdateFiles> UpdateFiles { get; set; } = updateFiles;
}