using Microsoft.Extensions.Configuration;

namespace FliGen.Services.Notifications
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
            configuration["TestConnection"] = 
                "Server=(localdb)\\mssqllocaldb;Database=FliGen.Notifications.Test;Trusted_Connection=True;MultipleActiveResultSets=true";
        }
    }
}