namespace ProcessingService.Configuration;

public sealed class NsbConnectionOptions
{
    public NsbEndPoint[] EndPoints { get; set; } = default!;
}