using AlbertTest.Entities.ApplicationDbContext;
using AlbertTest.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AlbertTest.Extensions
{
    public static class IdentityServiceExtension
    {
        //I created an Extension folder where I can add the services in the folder instead of adding every thing to the Program class which is more cleaner :)
        //I have added a lot of services in the Program class but I wanted to show that we can also add all the extensions needed in this way which will make it much better :)
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<AppUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication();

            return services;
        }
    }
}
