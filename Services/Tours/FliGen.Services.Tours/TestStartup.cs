using Microsoft.Extensions.Configuration;

namespace FliGen.Services.Tours
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
            configuration["TestConnection"] = 
                "Server=(localdb)\\mssqllocaldb;Database=FliGen.Tours.Test;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
    }
}