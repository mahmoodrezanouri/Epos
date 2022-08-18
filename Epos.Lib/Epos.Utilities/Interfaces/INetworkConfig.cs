namespace Epos.Utilities.Interfaces
{
    public interface INetworkConfig : IPrinterConfig
    {
        string IpAddress { get; set; }
        int Port { get; set; }
        string PrinterName { get; set; }
        int TimeOut { get; set; }

    }
}
