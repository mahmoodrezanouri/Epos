using Epos.Utilities.Printers;
using System.Collections.Generic;
using Epos.Utilities.Commands;
using Epos.Utilities.Printers.Configs;

namespace Epos.Utilities.Adapter.Model
{
    public class EpsonPrinterModels : BasePrinterModel
    {

        public static EpsonPrinterModels TML90i = new EpsonPrinterModels(0, "TM-L90-i", typeof(XmlPrintCommand).Name, typeof(NetworkPrinter).Name, typeof(NetworkConfig).Name);
        public static EpsonPrinterModels TMT88IV = new EpsonPrinterModels(1, "TM-T88IV", typeof(SimplePrintCommand).Name, typeof(NetworkPrinter).Name, typeof(NetworkConfig).Name);
        public static EpsonPrinterModels TMT20II = new EpsonPrinterModels(2, "TM-T20II", string.Empty, string.Empty, string.Empty);

        public EpsonPrinterModels(int id, string name, string typeName, string printerType, string configType)
          : base(id, name, typeName, printerType, configType)
        {

        }

        public static IEnumerable<EpsonPrinterModels> List()
        {
            var list = new[] {

               TML90i,
               TMT88IV,
               TMT20II
           };

            return list;
        }

    }
}

