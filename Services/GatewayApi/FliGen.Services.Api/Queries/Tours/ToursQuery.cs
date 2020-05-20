using FliGen.Common.Types;

namespace FliGen.Services.Api.Queries.Tours
{
    public class ToursQuery : PagedQuery
    {
        public int PlayerId { get; set; }
        public int? Last { get; set; }
        public ToursQueryType QueryType { get; set; }
        public int[] SeasonIds { get; set; }
    }
}