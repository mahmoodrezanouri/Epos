using Integration.Events.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Epos.Service.BuildingBlocks.EventBus.Abstractions;

namespace SignalrHub.IntegrationEvents
{
    public class PrinterStatusChangedEventHandler : IIntegrationEventHandler<PrinterStatusChangedEvent>
    {
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly ILogger<PrinterStatusChangedEventHandler> _logger;

        public PrinterStatusChangedEventHandler(
            IHubContext<NotificationsHub> hubContext,
            ILogger<PrinterStatusChangedEventHandler> logger)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task Handle(PrinterStatusChangedEvent @event)
        {
            await _hubContext.Clients.All.SendAsync("broadcastMessage",  @event.PrintOutput );
        }
    }
}

