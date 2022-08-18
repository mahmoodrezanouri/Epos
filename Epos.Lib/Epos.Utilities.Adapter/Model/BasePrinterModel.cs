using Epos.Utilities.Infrastructure;

namespace Epos.Utilities.Adapter.Model
{
    public class BasePrinterModel : Enumeration
    {
        protected static string _commandNameSpace => "Epos.Utilities.Commands";
        protected static string _configNameSpace => "Epos.Utilities.Printers.Configs";
        protected static string _printerTypeNameSpace => "Epos.Utilities.Printers";

        public string TypeName { get; set; }
        public string PrinterType { get; set; }
        public string ConfigType { get; set; }

        public BasePrinterModel(int id, string name)
            : base(id, name)
        {

        }
        public BasePrinterModel(int id, string name, string typeName, string printerType, string configType)
            : base(id, name)
        {
            TypeName = $"{_commandNameSpace}.{typeName}";
            PrinterType = $"{_printerTypeNameSpace}.{printerType}";
            ConfigType = $"{_configNameSpace}.{configType}";
        }
    
       
    }
}
