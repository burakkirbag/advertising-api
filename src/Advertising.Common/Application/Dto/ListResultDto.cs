using System.Collections.Generic;

namespace Advertising.Application.Dto
{
    public class ListResultDto<T>
    {
        public int TotalCount { get; }
        public List<T> Items { get; }

        public ListResultDto(int totalCount, List<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}
