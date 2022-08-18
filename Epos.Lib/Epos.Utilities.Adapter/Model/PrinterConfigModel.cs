using Epos.Utilities.Adapter.Interfaces;


namespace Epos.Utilities.Adapter.Model
{
    public class PrinterConfig : IPrinterAdapterConfig
    {
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public int SerialPort { get; set; }
        public string PrinterName { get; set; }
        public int TimeOut { get; set; }
        public int BufferSize { get; set; }

    }
}
