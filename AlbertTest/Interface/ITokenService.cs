using AlbertTest.Entities.Identity;

namespace AlbertTest.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
       
    }
}
