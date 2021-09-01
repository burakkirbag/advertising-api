namespace Advertising.Application.Queries
{
    public abstract class QueryBase<TResult> : IQuery<TResult>
    {
        public int Page { get; }
        public int PageSize { get; }
        public string Sort { get; }
        public string SortType { get; }

        protected QueryBase(int page, int pageSize, string sort, string sortType)
        {
            Page = page;
            PageSize = pageSize;
            Sort = sort;
            SortType = sortType;

            if (page <= 0) Page = 1;
            if (pageSize > 25 || pageSize == default(int)) PageSize = 10;
        }
    }
}
