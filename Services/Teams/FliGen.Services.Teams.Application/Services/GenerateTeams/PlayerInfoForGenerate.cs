namespace FliGen.Services.Teams.Application.Services.GenerateTeams
{
    public class PlayerInfoForGenerate
    {
        public int Id { get; }
        public double Rate { get; }
        public int LeaguePlayerPriority { get; }

        public PlayerInfoForGenerate(int id, double rate, int leaguePlayerPriority)
        {
            Id = id;
            Rate = rate;
            LeaguePlayerPriority = leaguePlayerPriority;
        }
    }
}