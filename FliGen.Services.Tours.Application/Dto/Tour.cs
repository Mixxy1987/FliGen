using FliGen.Services.Tours.Application.Dto.Enum;

namespace FliGen.Services.Tours.Application.Dto
{
    public class Tour
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public string Date { get; set; }
        public int HomeCount { get; set; }
        public int GuestCount { get; set; }
        public TourStatus TourStatus { get; set; }
    }
}
