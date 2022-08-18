using Epos.Utilities.Interfaces;
using Epos.Utilities.Printers;
using System.Collections.Generic;
using System.Linq;
using Epos.Utilities.Adapter.Extensions;
using Epos.Utilities.Infrastructure;
using Epos.Utilities.Adapter.Interfaces;
using System;

namespace Epos.Utilities.Adapter.Model
{
    public class RegisteredPrinterModels : Enumeration
    {
        public static Dictionary<string , IEnumerable<BasePrinterModel>> PrinterModels = new Dictionary<string, IEnumerable<BasePrinterModel>>();

        public static RegisteredPrinterModels EPSON = new RegisteredPrinterModels(0, nameof(EPSON), EpsonPrinterModels.List());
        public static RegisteredPrinterModels Cannon = new RegisteredPrinterModels(1, nameof(Cannon), CannonPrinterModels.List());

        public RegisteredPrinterModels(int id, string name, IEnumerable<BasePrinterModel> printerModels)
        : base(id, name)
        {
            PrinterModels.Add(name , printerModels);
        }

        public static IEnumerable<RegisteredPrinterModels> List()
        {
            var list = new List<RegisteredPrinterModels>() {

               EPSON,
               Cannon,
           };

            return list;
        }
        public static BasePrinterModel FindPrinter(string brandName , string modelName)
        {
            var printers = PrinterModels[brandName];

            if (printers == null)
                throw new ArgumentException($"Possible values for PrinterModels : {string.Join(",", List().Select(s => s.Name))}");

            var printer = printers.FirstOrDefault(x => x.Name == modelName);

            if (printer == null)
                throw new ArgumentException($"Printer Not Found!");

            return printer;
        }
        public static BasePrinter CreatePrinterInstance(IPrinterAdapterConfig printerConfig)
        {
            var printerInfo = FindPrinter(printerConfig.BrandName ,printerConfig.ModelName);

            var config = ObjectHelper.GetInstance(printerInfo.ConfigType, null);

            ObjectHelper.Map(printerConfig, config);

            var printerType = ObjectHelper.GetInstance(printerInfo.PrinterType, config) as BasePrinter;
            var printerModel = ObjectHelper.GetInstance(printerInfo.TypeName, null) as IPrintCommand;

            printerType.Set(printerModel);

            return printerType;
        }

    }
}

