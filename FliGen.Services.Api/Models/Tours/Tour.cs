namespace FliGen.Services.Api.Models.Tours
{
    public class Tour
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public string Date { get; set; }
        public int HomeCount { get; set; }
        public int GuestCount { get; set; }
        public int TourStatus { get; set; }
    }
}