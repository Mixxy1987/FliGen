using Microsoft.Extensions.Configuration;

namespace FliGen.Services.Players
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
            configuration["TestConnection"] = 
                "Server=(localdb)\\mssqllocaldb;Database=FliGen.Players.Test;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
    }
}