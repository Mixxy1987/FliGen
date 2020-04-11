namespace FliGen.Services.Teams.Application.Services.GenerateTeams
{
    public interface IGenerateTeamsService
    {
        int[][] Generate(InfoForGenerate info);
    }
}