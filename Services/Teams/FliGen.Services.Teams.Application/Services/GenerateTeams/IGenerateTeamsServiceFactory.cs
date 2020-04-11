using FliGen.Services.Teams.Application.Commands.GenerateTeams;

namespace FliGen.Services.Teams.Application.Services.GenerateTeams
{
    public interface IGenerateTeamsServiceFactory
    {
        IGenerateTeamsService Create(GenerateTeamsStrategy strategy);
    }
}