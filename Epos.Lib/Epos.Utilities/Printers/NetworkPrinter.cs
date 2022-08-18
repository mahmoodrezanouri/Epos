using Epos.Utilities.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Epos.Utilities.Printers
{
    public class NetworkPrinter : BasePrinter
    {
        private INetworkConfig _networkConfig;
        private IPrintCommand _command;
        protected override bool IsConnected => false;

        public NetworkPrinter(IPrinterConfig config)
            : base()
        {
            _networkConfig = config as INetworkConfig;
        }
        public override void Set(IPrintCommand command)
        {
            _command = command;
            command.SetConfig(_networkConfig);
        }
        public override void Print(IFluentPrintDocumentBuilder document)
        {
            ShowPrintData showPrintData = PrintOutput;
            _command.Print(document, showPrintData);
          
        }
        protected override void Connect()
        {

        }
        protected override void Reconnect()
        {
            
        }

    

    }
}
