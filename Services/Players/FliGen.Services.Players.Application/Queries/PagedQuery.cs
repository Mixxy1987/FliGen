namespace FliGen.Services.Players.Application.Queries
{
    public abstract class PagedQuery
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}