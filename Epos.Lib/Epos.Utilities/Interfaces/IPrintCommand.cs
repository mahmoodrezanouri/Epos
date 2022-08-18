using Epos.Utilities.Printers;

namespace Epos.Utilities.Interfaces
{
    public interface IPrintCommand
    {
        void Print(string data);

        void Print(IFluentPrintDocumentBuilder document);

        void SetConfig(IPrinterConfig config);

        void Print(IFluentPrintDocumentBuilder document, ShowPrintData showPrintData);
    }
}
