
using System.Threading.Tasks;
using Epos.Service.BuildingBlocks.EventBus.Abstractions;

namespace Epos.Service.BuildingBlocks.EventBus.Tests
{
    public class TestIntegrationOtherEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
    {
        public bool Handled { get; private set; }

        public TestIntegrationOtherEventHandler()
        {
            Handled = false;
        }

        public async Task Handle(TestIntegrationEvent @event)
        {
            Handled = true;
        }
    }
}
