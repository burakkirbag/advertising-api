using MassTransit;

namespace Advertising.Bus
{
    public interface IBusConsumer<T> : IConsumer<T> where T : class, IBusMessage
    {
    }
}
