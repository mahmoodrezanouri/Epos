using Integration.Events.EventHandling;
using Integration.Events.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Epos.Service.BuildingBlocks.EventBus.Abstractions;

namespace Epos.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEventBus _eventBus;

        public Worker(ILogger<Worker> logger, IEventBus eventBus, IHost host)
        {
            _logger = logger;
            _eventBus = eventBus;
            _eventBus.Subscribe<PrintStartedIntegrationEvent,PrintStartedIntegrationEventHandler>();

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
