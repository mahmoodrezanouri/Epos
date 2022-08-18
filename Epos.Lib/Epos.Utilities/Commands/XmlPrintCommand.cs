using Epos.Utilities.Infrastructure.Helper;
using Epos.Utilities.Interfaces;
using System;
using System.Net;
using System.Xml.Linq;
using Epos.Utilities.Infrastructure.Extensions;
using Epos.Utilities.Printers;

namespace Epos.Utilities.Commands
{
    public partial class XmlPrintCommand : IPrintCommand
    {
        private string address = string.Empty;
        private INetworkConfig _networkConfig;

        public void Print(string data)
        {
        }
        public void Print(IFluentPrintDocumentBuilder document)
        {
           var request = document.ToXmlRequest("EPSON");
            SendRequestToPrinter(request);
        }

        public void Print(IFluentPrintDocumentBuilder document, ShowPrintData showPrintData)
        {
            var request = document.ToXmlRequest("EPSON");
            showPrintData($"Printer : {_networkConfig.PrinterName}{Environment.NewLine}IP : {_networkConfig.IpAddress }{Environment.NewLine}Print Data : {request.ToString()}");
           // SendRequestToPrinter(request);
        }
        public void SetConfig(IPrinterConfig config)
        {
            _networkConfig = config as INetworkConfig;
            address = $"http://{_networkConfig.IpAddress}/cgi-bin/epos/service.cgi?devid={_networkConfig.PrinterName}&timeout={_networkConfig.TimeOut}";
        }
        private void SendRequestToPrinter(XElement req)
        {
            WebClient client = new WebClient();
            client.Headers["Content-Type"] = "text/xml; charset=utf-8";
            var result = client.UploadString(new Uri(address, UriKind.Absolute), req.ToString());
        }

    }
}
