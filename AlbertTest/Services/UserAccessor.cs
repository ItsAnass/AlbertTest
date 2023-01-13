using AlbertTest.Interface;
using System.Security.Claims;

namespace AlbertTest.Services
{
    public class UserAccessor : IUserAccessor
    {
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
