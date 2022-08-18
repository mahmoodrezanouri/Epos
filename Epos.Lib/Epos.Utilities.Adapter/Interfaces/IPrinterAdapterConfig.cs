using Epos.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epos.Utilities.Adapter.Interfaces
{
    public interface IPrinterAdapterConfig
    {
        string ModelName { get; set; }
        string BrandName { get; set; }
        string IpAddress { get; set; }
        int Port { get; set; }
        public int SerialPort { get; set; }
        string PrinterName { get; set; }
        int TimeOut { get; set; }
        int BufferSize { get; set; }

    }
}
