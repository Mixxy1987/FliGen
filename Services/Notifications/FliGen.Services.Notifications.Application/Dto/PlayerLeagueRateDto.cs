namespace FliGen.Services.Notifications.Application.Dto
{
    public class PlayerLeagueRateDto
    {
        public string Date { get; set; }
        public int PlayerId { get; set; }
        public int LeagueId { get; set; }
        public double Rate { get; set; }
    }
}
