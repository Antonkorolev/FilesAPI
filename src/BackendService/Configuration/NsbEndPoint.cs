namespace BackendService.Configuration;

public sealed class NsbEndPoint
{
    public string Host { get; set; } = default!;

    public int Port { get; set; }

    public string VirtualHost { get; set; } = default!;

    public string Login { get; set; } = default!;

    public string Password { get; set; } = default!;
}