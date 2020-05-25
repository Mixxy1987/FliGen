namespace FliGen.Common.Types
{
    public abstract class PagedQuery
    {
        private const int DefaultSize = 20;
        private const int DefaultPage = 0;

        public int Page { get; set; }
        public int Size { get; set; }

        protected PagedQuery(int? size, int? page)
        {
            Size = size ?? DefaultSize;
            Page = page ?? DefaultPage;
        }

        protected PagedQuery()
        {
            Size = DefaultSize;
            Page = DefaultPage;
        }
    }
}