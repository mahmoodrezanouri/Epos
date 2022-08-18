using Integration.Events.Events;
using System;
using System.Threading.Tasks;
using Epos.Service.BuildingBlocks.EventBus.Abstractions;
using Epos.Utilities.Printers;

namespace Integration.Events.EventHandling
{
    public class PrintStartedIntegrationEventHandler : IIntegrationEventHandler<PrintStartedIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        public PrintStartedIntegrationEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task Handle(PrintStartedIntegrationEvent @event)
        {
            @event.PrintAdapter.StatusChanged += StatusChanged;
            @event.PrintAdapter.SendPrintRequest();
        }
        private  void StatusChanged(object sender, EventArgs ps)
        {
            var printerStatus = (PrinterStatusEventArgs)ps;
            System.Diagnostics.Debug.WriteLine($"{printerStatus.PrintData}");
            System.Diagnostics.Debug.WriteLine("============================================");

            var @event = new PrinterStatusChangedEvent(printerStatus.PrintData);

            _eventBus.Publish(@event);
        }
    }
}

