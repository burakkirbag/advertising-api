using Advertising.Application.Commands;
using Advertising.Bus;
using Advertising.Bus.Contracts.Messages;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Advertising.Adverts.Commands.AddVisit
{
    public class AddVisitCommandHandler : ICommandHandler<AddVisitCommand>
    {
        private readonly IBusService _busService;

        public AddVisitCommandHandler(IBusService busService)
        {
            _busService = busService;
        }

        public Task<Unit> Handle(AddVisitCommand request, CancellationToken cancellationToken)
        {
            var message = new AddAdvertVisitMessage(request.AdvertId, request.VisitorIpAddress, request.Date);
            _busService.Send(Consts.ADD_ADVERT_VISIT_QUEUE_NAME, message);
            return Unit.Task;
        }
    }
}
