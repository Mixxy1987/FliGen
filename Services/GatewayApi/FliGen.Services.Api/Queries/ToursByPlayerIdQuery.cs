namespace FliGen.Services.Api.Queries
{
    public class ToursByPlayerIdQuery : PagedQuery
    {
        public int PlayerId { get; set; }
        public int QueryType { get; set; }
        public int[] SeasonId { get; set; }
    }
}