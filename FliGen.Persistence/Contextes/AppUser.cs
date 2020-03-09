using Microsoft.AspNetCore.Identity;

namespace FliGen.Persistence.Contextes
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
    }
}
