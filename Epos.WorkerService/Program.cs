using Integration.Events.EventHandling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epos.Service.BuildingBlocks.EventBus;
using Epos.Service.BuildingBlocks.EventBus.Abstractions;
using Epos.Service.EventBus.EventBusMSMQ;
using Epos.Utilities.Adapter.Interfaces;
using Epos.Utilities.Adapter.Model;

namespace Epos.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<Worker>();
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<PrintStartedIntegrationEventHandler>();
            services.AddSingleton<IEventBus, EventBusMSMQ>();

        })
            .UseWindowsService();


    }
}
