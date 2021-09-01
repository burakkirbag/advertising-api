using Advertising.Adverts.Services;
using Advertising.Bus;
using Advertising.Bus.Contracts.Messages;
using MassTransit;
using System.Threading.Tasks;

namespace Advertising.Api.Workers.Bus.Consumers
{
    public class AddAdvertVisitConsumer : IBusConsumer<AddAdvertVisitMessage>
    {
        private readonly IAdvertVisitService _advertVisitService;

        public AddAdvertVisitConsumer(IAdvertVisitService advertVisitService)
        {
            _advertVisitService = advertVisitService;
        }

        public async Task Consume(ConsumeContext<AddAdvertVisitMessage> context)
        {
            await _advertVisitService.Create(context.Message.AdvertId, context.Message.VisitorIpAddress, context.Message.Date);
        }
    }
}
