using Experimental.System.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Epos.Service.BuildingBlocks.EventBus;
using Epos.Service.BuildingBlocks.EventBus.Abstractions;
using Epos.Service.BuildingBlocks.EventBus.Events;

namespace Epos.Service.EventBus.EventBusMSMQ
{
    public class EventBusMSMQ : IEventBus
    {
        private MessageQueue mq;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private string _queueName;
        private IHost _host;
        public EventBusMSMQ( IEventBusSubscriptionsManager subsManager,string queueName = null)
        {
            _queueName = queueName;
            _subsManager = subsManager;
        }

        public EventBusMSMQ(IEventBusSubscriptionsManager subsManager, IHost host ,string queueName = null)
        {
            _queueName = queueName;
            _subsManager = subsManager;
            _host = host;
        }
        public EventBusMSMQ(string queueName = null)
        {
            _queueName = queueName;
        }

        public void Publish(IntegrationEvent @event)
        {
            SetInternalQueue();

            var eventName = @event.GetType().Name;

            Message mm = new Message();
            new JsonMessageFormatter().Write(mm, @event);

            mm.Label = eventName;
            mq.Send(mm);
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH :IIntegrationEventHandler<T>
        {

            var eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);
            _subsManager.AddSubscription<T, TH>();
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
           
        }
        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            throw new NotImplementedException();
        }

        private void Receive_Completed_EventHandler(object sender, ReceiveCompletedEventArgs receiveCompletedEventArgs)
        {
            try
            {
                MessageQueue mq = (MessageQueue)sender;
                Message mes = mq.EndReceive(receiveCompletedEventArgs.AsyncResult);
                var eventName = mes.Label;
                ProcessEvent(eventName , mes);
            }
            catch (MessageQueueException)
            {
                // Handle sources of MessageQueueException.
            }

            return;
        }

        private void ProcessEvent(string eventName, Message message)
        {

            var subscriptions = _subsManager.GetHandlersForEvent(eventName);

            foreach (var subscription in subscriptions)
            {
                var eventType = _subsManager.GetEventTypeByName(eventName);

                using (var scope = _host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    var integrationEvent = new JsonMessageFormatter(eventType , message).Read();

                    var eventHandler = services.GetRequiredService(subscription.HandlerType);
                    concreteType.GetMethod("Handle").Invoke(eventHandler, new object[] { integrationEvent });

                }

            }
        }
        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                SetInternalQueue();
            }
        }

        public void BeginReceive()
        {

            MessageQueue myQueue = new MessageQueue($@".\Private$\{_queueName}");

            myQueue.ReceiveCompleted +=
               Receive_Completed_EventHandler;

           myQueue.BeginReceive();
        }
        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {

        }

        private void SetInternalQueue()
        {
            if (MessageQueue.Exists($@".\Private$\{_queueName}"))
                mq = new MessageQueue($@".\Private$\{_queueName}");
            else
                mq = MessageQueue.Create($@".\Private$\{_queueName}");
        }

    }
}
