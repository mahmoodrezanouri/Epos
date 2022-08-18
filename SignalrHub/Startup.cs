using Integration.Events.EventHandling;
using Integration.Events.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalrHub.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epos.Service.BuildingBlocks.EventBus;
using Epos.Service.BuildingBlocks.EventBus.Abstractions;
using Epos.Service.EventBus.EventBusMSMQ;

namespace SignalrHub
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder
                                      .AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials()
                                      // .WithOrigins("*");
                                      .WithOrigins("https://localhost:44345",
                                                          "http://localhost:44345/");
                                  });
            });

            services.AddSignalR();
            RegisterEventBus(services);
            services.AddHostedService<EventBusWorker>();
            services.AddOptions();

        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationsHub>("/hub/notificationhub");
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<PrintStartedIntegrationEvent, PrintStartedIntegrationEventHandler>();
            eventBus.Subscribe<PrinterStatusChangedEvent, PrinterStatusChangedEventHandler>();
        }

        private void RegisterEventBus(IServiceCollection services)
        {
            services.AddSingleton<IEventBus, EventBusMSMQ>(sp =>
            {
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var iHost = sp.GetRequiredService<IHost>();
                return new EventBusMSMQ(eventBusSubcriptionsManager , iHost, queueName : "PrintQ");
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<PrintStartedIntegrationEventHandler>();
            services.AddTransient<PrinterStatusChangedEventHandler>();

        }
    }
}
