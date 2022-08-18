using Epos.Utilities.Adapter.Interfaces;
using Epos.Utilities.Infrastructure;
using Epos.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using Epos.Utilities.Adapter.Model;
using Newtonsoft.Json;

namespace Epos.Utilities.Adapter
{
    public class PrintAdapter : IPrintAdapter
    {
        [JsonProperty]
        private IPrinterAdapterConfig _config { get; set; }
        [JsonProperty]
        private IFluentPrintDocumentBuilder _document { get; set; }

        public event EventHandler StatusChanged;

        public IEnumerable<PrinterModel> GetSupportedPrintersList()
        {
            //var result = RegisteredPrinterModels.List();

            //return result.Select(x => new PrinterModel { 

            //    Id = x.Id,
            //    Model = x.Name,
            //    PrinterType = x.PrinterType

            //});

            return null;
        }
        public void SendPrintRequest()
        {
            var targetPrinter = RegisteredPrinterModels.CreatePrinterInstance(_config);
            targetPrinter.StatusChanged += StatusChanged;
            targetPrinter.Print(_document);
        }
        public void SetPrintDocument(IFluentPrintDocumentBuilder document)
        {
            _document = document;
        }
        public void SetPrinterConfig(IPrinterAdapterConfig config)
        {
            _config = config;
        }
    }
}
