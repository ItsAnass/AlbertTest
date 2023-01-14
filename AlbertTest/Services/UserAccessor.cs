using AlbertTest.Interface;
using System.Security.Claims;

namespace AlbertTest.Services
{
    public class UserAccessor : IUserAccessor
    {

        //Accessing HttpContext in order to get the current details of the loged in USER from his generated token :)

        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUser()
        {
            var userName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>x.Type == ClaimTypes.NameIdentifier)?.Value;

            return userName;
        }
    }
}
