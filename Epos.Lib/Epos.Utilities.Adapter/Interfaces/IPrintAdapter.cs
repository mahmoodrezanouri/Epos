using Epos.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Epos.Utilities.Adapter.Model;

namespace Epos.Utilities.Adapter.Interfaces
{
    public interface IPrintAdapter : IEposAdapter
    {
        public void SetPrintDocument(IFluentPrintDocumentBuilder document);
        public void SetPrinterConfig(IPrinterAdapterConfig config);

        public event EventHandler StatusChanged;
        public void SendPrintRequest();

        public IEnumerable<PrinterModel> GetSupportedPrintersList();
    }
}
