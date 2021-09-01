using System;
using System.Threading.Tasks;

namespace Advertising.Bus
{
    public interface IBusService : IDisposable
    {
        Task Publish<T>(string topicOrExchangeName, T message) where T : class, IBusMessage;

        Task Send<T>(string queueName, T message) where T : class, IBusMessage;

        Task SubscribeFor<T>(string topicOrExchangeName, IBusConsumer<T> consumer, int? concurrentMessageLimit = null) where T : class, IBusMessage;

        Task SubscribeFor<T>(string queueName, IBusConsumer<T> consumer, int? concurrentMessageLimit = null, int? prefetchCount = null) where T : class, IBusMessage;

        Task Stop();
    }
}
