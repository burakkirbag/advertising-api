using Advertising.Bus;
using Advertising.Bus.Contracts.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Advertising.Api.Workers.Bus
{
    public class BusWorker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public BusWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var bus = scope.ServiceProvider.GetRequiredService<IBusService>();
                var consumer = scope.ServiceProvider.GetRequiredService<IBusConsumer<AddAdvertVisitMessage>>();

                await bus.SubscribeFor(Consts.ADD_ADVERT_VISIT_QUEUE_NAME, consumer, null, null);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
