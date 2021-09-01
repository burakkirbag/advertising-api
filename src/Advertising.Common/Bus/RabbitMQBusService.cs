using Advertising.Configuration;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.RabbitMqTransport.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Advertising.Bus
{
    public class RabbitMQBusService : BusServiceBase, IBusService
    {
        public RabbitMQBusService(IOptions<BusConfig> configuration) : base(configuration)
        {
            BusControl = MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(Configuration.Host, h =>
                {
                    h.Username(Configuration.UserName);
                    h.Password(Configuration.Password);
                });
            });
        }

        protected override void ConfigureReviceEndpoint(string topicOrExchangeName, ref IReceiveEndpointConfigurator receiveEndpointConfigurator)
        {
            if (receiveEndpointConfigurator is RabbitMqReceiveEndpointConfiguration rabbitMqReceiveEndpointConfiguration)
            {
                rabbitMqReceiveEndpointConfiguration.Bind(topicOrExchangeName, (ebc) =>
                {
                });
            }
        }

        protected override void ConfigureReviceEndpoint(int? prefetchCount, ref IReceiveEndpointConfigurator receiveEndpointConfigurator)
        {
            if (receiveEndpointConfigurator is RabbitMqReceiveEndpointConfiguration rabbitMqReceiveEndpointConfiguration)
            {
                if (prefetchCount.HasValue)
                {
                    rabbitMqReceiveEndpointConfiguration.PrefetchCount = Convert.ToUInt16(prefetchCount.Value);
                }
            }
        }

        protected override Uri GetPublishEndpointUri(string topicOrExchangeName)
        {
            return new RabbitMqEndpointAddress(BusControl.Address, topicOrExchangeName);
        }

        protected override Uri GetSendEndpointUri(string queueName)
        {
            return new RabbitMqEndpointAddress(BusControl.Address, queueName);
        }
    }
}
