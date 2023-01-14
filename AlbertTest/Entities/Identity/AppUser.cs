using Microsoft.AspNetCore.Identity;

namespace AlbertTest.Entities.Identity
{
    //I inherited IdentityUser in order to work with the user Authentication
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
