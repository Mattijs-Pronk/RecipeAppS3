using Back_end_API.BusinessLogic;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_API.Controllers
{
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
            var newUser = new UserModel
            {
                userName = request.userName,
                Email = request.Email,
                passwordHash = request.passwordHash,
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
                .FirstOrDefault(u => u.Email == request.email);

            bool validPassword = BCrypt.Net.BCrypt.Verify(request.password, Myuser.passwordHash);

            if (Myuser == null || validPassword == false)
            {
                return BadRequest("user not found");
            }

            return Ok(Myuser.userId);
        }
    }
}
