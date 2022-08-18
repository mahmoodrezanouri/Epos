using Integration.Events.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epos.Service.BuildingBlocks.EventBus.Abstractions;
using Epos.Utilities.Adapter;
using Epos.Utilities.Adapter.Infrastructure;
using Epos.Utilities.Adapter.Model;

namespace PrintService.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrintController : ControllerBase
    {

        private readonly ILogger<PrintController> _logger;

        private readonly IEventBus _eventBus;
        public PrintController(ILogger<PrintController> logger , IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }
        [HttpPost]
        public void TestPrint()
        {
            Print();
        }

        private void Print()
        {
            var epsonAdpter = new PrintAdapter();
            var epsonConfig = new PrinterConfig()
            {
                ModelName = "TM-L90-i",
                BrandName = "EPSON",
                IpAddress = "192.168.7.12",
                PrinterName = "Ep-test",
                Port = 80,
                TimeOut = 120
            };

            epsonAdpter.SetPrinterConfig(epsonConfig);
            var epsonDocument = PrintDocumentBuilder.Start()

                    .PrintLine("Its a Test Print!")
                    .SetStyle(x => { x.Height = "100px"; x.Width = "45px"; })
                    .PrintLine("Print Hello World !!!");

            epsonAdpter.SetPrintDocument(epsonDocument);

            var @event = new PrintStartedIntegrationEvent(epsonAdpter);

            _eventBus.Publish(@event);
        }
    }
}
