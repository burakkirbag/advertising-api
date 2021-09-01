using MediatR;

namespace Advertising.Application.Commands
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }

    public interface ICommand : IRequest
    {

    }
}
