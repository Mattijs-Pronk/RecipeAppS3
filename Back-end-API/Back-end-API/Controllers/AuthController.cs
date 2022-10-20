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
        public readonly RecipeAppContext _context;

        public AuthController(RecipeAppContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(UserModel request)
        {
            //var doubleEmail = _context.Users.Any(u => u.Email == request.Email);
            //var doubleUsername = _context.Users.Any(u => u.userName == request.userName);

            //if(doubleEmail || doubleUsername)
            //{
            //    return BadRequest("userName or Email already exists");
            //}

            var newUser = new UserModel
            {
                userName = request.userName,
                Email = request.Email,
                passwordHash = request.passwordHash,
                isAdmin = false,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserDTO request)
        {
            var Myuser = _context.Users
                .FirstOrDefault(u => u.Email == request.email);

            if(Myuser != null)
            {
                bool validPassword = BCrypt.Net.BCrypt.Verify(request.password, Myuser.passwordHash);

                if(validPassword)
                {
                    return Ok(Myuser.userName);
                }
            }
            return NotFound("user not found");
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
