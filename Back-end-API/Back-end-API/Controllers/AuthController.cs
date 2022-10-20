using Back_end_API.BusinessLogic;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        VerifyInfo verify = new VerifyInfo();

        public readonly RecipeAppContext _context;

        public AuthController(RecipeAppContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(UserDTO request)
        {
            verify.CreatePasswordHash(request.Password, out byte[] passwordhash, out byte[] passwordsalt);

            var newUser = new UserModel
            {
                userName = request.UserName,
                Email = request.Email,
                passwordHash = passwordhash,
                passwordSalt = passwordsalt,
                isAdmin = request.isAdmin,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserDTO request)
        {
            var Myuser = _context.Users
                .FirstOrDefault(u => u.Email == request.Email);

            if (Myuser != null)
            {
                if (verify.VerifyPasswordHash(request.Password, Myuser.passwordHash, Myuser.passwordSalt))
                {
                    return Ok(Myuser.userId);
                }
            }
            return BadRequest("user not found");
        }

        [HttpPost("checkname")]
        public async Task<ActionResult<bool>> UserNameChecker(string username)
        {
            bool doubleUsername = _context.Users.Any(u => u.userName == username);

            if (doubleUsername) { return true; }

            return false;
        }

        [HttpPost("checkemail")]
        public async Task<ActionResult<bool>> EmailChecker(string email)
        {
            bool doubleEmail = _context.Users.Any(u => u.Email == email);

            if (doubleEmail) { return true; }

            return false;
        }
    }
}
