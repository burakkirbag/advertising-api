using MediatR;

namespace Advertising.Application.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
