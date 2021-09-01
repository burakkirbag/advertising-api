using Advertising.Configuration;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Advertising.Bus
{
    public abstract class BusServiceBase : IBusService
    {
        private bool disposedValue;
        protected BusConfig Configuration { get; }
        protected IBusControl BusControl { get; set; }

        public BusServiceBase(IOptions<BusConfig> configuration)
        {
            Configuration = configuration.Value;
        }

        public virtual async Task Publish<T>(string topicOrExchangeName, T message) where T : class, IBusMessage
        {
            var endpoint = await GetPublishEndpoint(topicOrExchangeName);
            await endpoint.Send(message);
        }

        public virtual async Task Send<T>(string queueName, T message) where T : class, IBusMessage
        {
            var endpoint = await GetSendEndpoint(queueName);
            await endpoint.Send(message);
        }

        public virtual async Task SubscribeFor<T>(string topicOrExchangeName, IBusConsumer<T> consumer, int? concurrentMessageLimit) where T : class, IBusMessage
        {
            await Task.Run(() =>
            {
                BusControl.ConnectReceiveEndpoint((cfg) =>
                {
                    ConfigureReviceEndpoint(topicOrExchangeName, ref cfg);

                    cfg.Consumer(() => { return consumer; }, (consumerConfig) =>
                    {
                        if (concurrentMessageLimit.HasValue)
                        {
                            consumerConfig.UseConcurrencyLimit(concurrentMessageLimit.Value);
                        }
                    });
                });
            });
        }

        public virtual async Task SubscribeFor<T>(string queueName, IBusConsumer<T> consumer, int? concurrentMessageLimit, int? prefetchCount) where T : class, IBusMessage
        {
            await Task.Run(() =>
            {
                BusControl.ConnectReceiveEndpoint(queueName, (cfg) =>
                {
                    ConfigureReviceEndpoint(prefetchCount, ref cfg);

                    if (cfg is IConsumePipeConfigurator consumePipeConfigurator)
                    {
                        if (concurrentMessageLimit.HasValue)
                        {
                            consumePipeConfigurator.UseConcurrencyLimit(concurrentMessageLimit.Value);
                        }
                    }

                    cfg.Consumer(() => { return consumer; }, (consumerConfig) =>
                    {
                        if (concurrentMessageLimit.HasValue)
                        {
                            consumerConfig.UseConcurrencyLimit(concurrentMessageLimit.Value);
                        }
                    });
                });

                Uri uri;
                if (!EndpointConvention.TryGetDestinationAddress<T>(out uri))
                {
                    uri = new Uri(BusControl.Address, queueName);
                    EndpointConvention.Map<T>(GetSendEndpointUri(queueName));
                }
            });
        }

        protected virtual async Task<ISendEndpoint> GetPublishEndpoint(string topicOrExchangeName)
        {
            return await BusControl.GetSendEndpoint(GetPublishEndpointUri(topicOrExchangeName));
        }

        protected virtual async Task<ISendEndpoint> GetSendEndpoint(string queueName)
        {
            return await BusControl.GetSendEndpoint(GetSendEndpointUri(queueName));
        }

        protected abstract Uri GetSendEndpointUri(string queueName);

        protected abstract Uri GetPublishEndpointUri(string topicOrExchangeName);

        protected abstract void ConfigureReviceEndpoint(string topicOrExchangeName, ref IReceiveEndpointConfigurator receiveEndpointConfigurator);

        protected abstract void ConfigureReviceEndpoint(int? prefetchCount, ref IReceiveEndpointConfigurator receiveEndpointConfigurator);

        public async Task Stop()
        {
            await BusControl?.StopAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (BusControl != null)
                    {
                        BusControl.Stop();
                        BusControl = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        ~BusServiceBase()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
