using Epos.Service.BuildingBlocks.EventBus.Events;

namespace Integration.Events.Events
{
    public class PrinterStatusChangedEvent : IntegrationEvent
    {
        public string PrintOutput { get; }

        public PrinterStatusChangedEvent(string printOutput)
        {
            PrintOutput = printOutput;
        }
    }
}
