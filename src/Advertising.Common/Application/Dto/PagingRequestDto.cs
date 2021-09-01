namespace Advertising.Application.Dto
{
    public class PagingRequestDto
    {
        private int _pageSize = 10;
        private int _page = 1;

        public int PageSize
        {
            get { return _pageSize; }

            set
            {
                if (value > 0 && value <= 25)
                    _pageSize = value;
                else
                    _pageSize = 10;
            }
        }

        public int Page
        {
            get { return _page; }
            set
            {
                if (value > 0)
                    _page = value;
                else
                    _page = 1;
            }
        }
    }
}
