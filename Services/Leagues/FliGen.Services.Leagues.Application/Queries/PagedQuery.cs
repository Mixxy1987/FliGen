namespace FliGen.Services.Leagues.Application.Queries
{
    public abstract class PagedQuery
    {
        private const int DefaultSize = 20;

        public int Page { get; set; }
        public int Size { get; set; } = DefaultSize;
    }
}