using Epos.Utilities.Interfaces;
using System;
using Epos.Utilities.Printers;

namespace Epos.Utilities.Commands
{
    public partial class SimplePrintCommand : IPrintCommand
    {
       
        private INetworkConfig _networkConfig;

        public void Print(string data)
        {
        }
        public void Print(IFluentPrintDocumentBuilder document)
        {
           //var request = document.ToXmlRequest();
        }
        public void Print(IFluentPrintDocumentBuilder document, ShowPrintData showPrintData)
        {
            var printDoc = document.Build();

            var request = string.Empty;
                
            foreach (var doc in printDoc)
            {
                var textLineDocumentElement = (BaseTextLineDocumentElement)doc;
                request += textLineDocumentElement.Value;
            }

            showPrintData($"Printer : {_networkConfig.PrinterName}{Environment.NewLine}IP : {_networkConfig.IpAddress }{Environment.NewLine}Print Data : {request}");
        }

        public void SetConfig(IPrinterConfig config)
        {
            _networkConfig = config as INetworkConfig;
        }

    }
}
