using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalrHub.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Epos.Service.BuildingBlocks.EventBus.Abstractions;

namespace SignalrHub
{
    public class EventBusWorker : BackgroundService
    {
        private readonly ILogger<EventBusWorker> _logger;
        private readonly IEventBus _eventBus;

        public EventBusWorker(ILogger<EventBusWorker> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _eventBus.BeginReceive();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
