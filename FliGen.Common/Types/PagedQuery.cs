namespace FliGen.Common.Types
{
    public abstract class PagedQuery
    {
        private const int DefaultSize = 20;

        public int Page { get; set; }
        public int Size { get; set; } = DefaultSize;
    }
}