using System.Collections.Generic;

namespace Advertising.Application.Dto
{
    public class PagingResultDto<T> : ListResultDto<T>
    {
        public PagingResultDto(int pageSize, int currentPage, int totalCount, List<T> items) : base(totalCount, items)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            PageCount = TotalCount / PageSize;

            if (TotalCount % PageSize != 0)
                PageCount += 1;

            IsLastPage = CurrentPage >= PageCount;
        }

        public int PageCount { get; }
        public int PageSize { get; }
        public int CurrentPage { get; }
        public bool IsLastPage { get; }
    }
}
