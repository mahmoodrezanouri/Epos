using Epos.Utilities.Interfaces;


namespace Epos.Utilities.Printers.Configs
{
    public class NetworkConfig : INetworkConfig
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string PrinterName { get; set; }
        public int TimeOut { get; set; }
 
    }
}
