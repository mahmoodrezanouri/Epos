using Epos.Utilities.Printers;
using System.Collections.Generic;
using Epos.Utilities.Commands;
using Epos.Utilities.Printers.Configs;

namespace Epos.Utilities.Adapter.Model
{
    public class CannonPrinterModels : BasePrinterModel
    {
       
        public static CannonPrinterModels CP100 = new CannonPrinterModels(0, "CP-100", typeof(SimplePrintCommand).Name, typeof(NetworkPrinter).Name, typeof(NetworkConfig).Name);

        public CannonPrinterModels(int id, string name, string typeName, string printerType, string configType)
               : base(id, name, typeName, printerType, configType)
        {

        }
        public static IEnumerable<CannonPrinterModels> List()
        {
            var list = new[] {

               CP100
           };

            return list;
        }

    }
}

