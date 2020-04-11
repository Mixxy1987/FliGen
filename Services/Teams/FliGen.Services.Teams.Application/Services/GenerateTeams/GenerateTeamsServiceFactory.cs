using System;
using FliGen.Services.Teams.Application.Commands.GenerateTeams;

namespace FliGen.Services.Teams.Application.Services.GenerateTeams
{
    public class GenerateTeamsServiceFactory : IGenerateTeamsServiceFactory
    {
        public IGenerateTeamsService Create(GenerateTeamsStrategy strategy)
        {
            switch (strategy)
            {
                case(GenerateTeamsStrategy.Random):
                    return new RandomTeamsGenerator();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}