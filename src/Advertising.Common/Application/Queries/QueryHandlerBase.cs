using AutoMapper;
using System.Threading;
using System.Threading.Tasks;

namespace Advertising.Application.Queries
{
    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        protected IMapper Mapper { get; }

        public QueryHandlerBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        public abstract Task<TResult> Handle(TQuery request, CancellationToken cancellationToken);
    }
}
