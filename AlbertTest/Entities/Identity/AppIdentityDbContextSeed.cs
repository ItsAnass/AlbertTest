using Microsoft.AspNetCore.Identity;

namespace AlbertTest.Entities.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManger)
        {
            if (!userManger.Users.Any())
            {
                var user = new AppUser
                {
                    FirstName = "Anas",
                    LastName = "Aldaya",
                    Email = "Anas@test.com",
                    UserName = "Anas@test.com"
                };
                await userManger.CreateAsync(user, "Pa$$w0rd");
            }

           
        }
    }
}
