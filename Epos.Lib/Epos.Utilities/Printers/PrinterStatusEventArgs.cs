using System;

namespace Epos.Utilities.Printers
{
    public class PrinterStatusEventArgs : EventArgs
    {
        public string PrintData { get; set; }
    }

    public delegate void ShowPrintData(string printData);
}