namespace ProcessingService.Configuration;

public sealed class NsbEndPoint
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string VirtualHost { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }
}