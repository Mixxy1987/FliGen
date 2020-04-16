using Microsoft.AspNetCore.Identity;

namespace FliGen.Services.AuthServer.Persistence.Contexts
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
    }
}
