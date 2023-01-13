using AlbertTest.Dtos;
using AlbertTest.Entities.Identity;
using AlbertTest.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlbertTest.Controllers
{
    
    public class AccountController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUserAccessor _userAccessor;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IUserAccessor userAccessor ) 
        {
            _signInManager = signInManager;
            _userManager   = userManager;
            _tokenService  = tokenService;
            _userAccessor = userAccessor;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            };
                
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.DisplayName,
                FirstName= registerDto.FirstName,
                LastName= registerDto.LastName,

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest();

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName,
                FirstName= user.FirstName,
                LastName= user.LastName,
              
            };
            
        }
        [Authorize]
        [HttpGet("getUser")]
        public async Task<CurrentUserDto> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUser());

            return new CurrentUserDto
            {
                Email = user.Email,
                Id = user.Id,
                FirstName = user.FirstName,
            };
        }
    }
}
