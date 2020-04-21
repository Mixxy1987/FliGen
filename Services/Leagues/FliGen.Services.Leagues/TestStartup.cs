using Microsoft.Extensions.Configuration;

namespace FliGen.Services.Leagues
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
            configuration["TestConnection"] =
                "Server=(localdb)\\mssqllocaldb;Database=FliGen.Leagues.Test;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
    }
}