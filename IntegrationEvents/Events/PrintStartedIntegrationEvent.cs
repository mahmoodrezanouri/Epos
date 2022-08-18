using Newtonsoft.Json;
using Epos.Service.BuildingBlocks.EventBus.Events;
using Epos.Utilities.Adapter;
using Epos.Utilities.Adapter.Infrastructure;
using Epos.Utilities.Adapter.Interfaces;

namespace Integration.Events.Events
{
    public class PrintStartedIntegrationEvent : IntegrationEvent
    {
        [JsonProperty]
        public IPrintAdapter PrintAdapter { get; set; }

        [JsonConstructor]
        public PrintStartedIntegrationEvent()
        {

        }

        public PrintStartedIntegrationEvent(IPrintAdapter printAdapter)
        {
            PrintAdapter = printAdapter;
        }
       

    }
}
