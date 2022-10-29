using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.FavoriteDTO_s;
using Back_end_API.BusinessLogic.UserDTO_s;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_end_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly RecipeAppContext _context;

        VerifyInfo verify = new VerifyInfo();
        EmailCreator emailCreator = new EmailCreator();

        public UserController(RecipeAppContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("getuser")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUserById(int id)
        {
            var myuser = await _context.Users
            .Where(u => u.userId == id)
                         .Select(u => new
                         {
                             u.userId,
                             u.userName,
                             u.Email,
                             u.adress,
                             u.phone,
                             u.activeSince
                         })
                         .ToListAsync();

            return Ok(myuser);
        }

        [HttpGet("recipesint")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetUserRecipesAmountById(int id)
        {
            var createdrecipes = await _context.Recipes
                .Where(r => r.userId == id)
                .ToListAsync();

            var recipes = createdrecipes.Count();

            return Ok(recipes);
        }

        [HttpGet("favoritesint")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetUserFavoritesAmountById(int id)
        {
            var favoriterecipes = await _context.Favorites
                .Where(f => f.userId == id)
                .ToListAsync();

            var favorites = favoriterecipes.Count();

            return Ok(favorites);
        }

        [HttpPost("changeusername")]
        public async Task<ActionResult<bool>> UsernameCheckerChangeUsername(ChangeUsernameDTO request)
        {
            bool doubleUsername = await _context.Users.AnyAsync(u => u.userName == request.newUsername);

            if (doubleUsername && request.currentUsername != request.newUsername) { return true; }

            return false;
        }

        [HttpPut("changepassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangePassword(ChangeUserPassword request)
        {
            var myuser = await _context.Users.FindAsync(request.userId);
            if(myuser != null && verify.VerifyPasswordHash(request.currentPassword, myuser.passwordHash, myuser.passwordSalt))
            {
                verify.CreatePasswordHash(request.newPassword, out byte[] passwordhash, out byte[] passwordsalt);

                myuser.passwordHash = passwordhash;
                myuser.passwordSalt = passwordsalt;

                await _context.SaveChangesAsync();

                //stuur email met confirmatie wachtwoord.
                emailCreator.SendEmailResetPasswordSucces(myuser.Email, myuser.userName);

                return Ok("password has changed");
            }
            return BadRequest("incorrect password");
        }

        [HttpPut("changeprofile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangeProfile(ChangeUserProfileDTO request)
        {
            var myuser = await _context.Users.FindAsync(request.userId);
            if (myuser != null)
            {
                if (request.userName == "") { myuser.userName = myuser.userName; }
                else { myuser.userName = request.userName; }
                if (request.adress == "") { myuser.adress = myuser.adress; }
                else { myuser.adress = request.adress; }
                if (request.phone == "") { myuser.phone = myuser.phone; }
                else { myuser.phone = request.phone; }

                await _context.SaveChangesAsync();
                return Ok("profile has changed");
            }
            return BadRequest("user not found");
        }

        [HttpGet("getmyrecipes")]
        [ProducesResponseType(typeof(RecipeModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUserRecipesById(int id)
        {
            var myrecipe = await _context.Recipes
                         .Where(r => r.userId == id)
                         .Select(r => new
                         {
                             r.recipeId,
                             r.Title,
                             r.Description,
                             r.Ingredients,
                             r.Rating,
                             r.prepTime,
                             r.Portions,
                             r.User.userName
                         })
                         .ToListAsync();

            return Ok(myrecipe);
        }

        [HttpPost("addfavorite")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddToFavorites(AddFavoriteDTO request)
        {
            var myuser = await _context.Users.FindAsync(request.userId);
            var myrecipe = await _context.Recipes.FindAsync(request.recipeId);
            var myfavorites = await _context.Favorites.FirstOrDefaultAsync(f => f.userId == request.userId && f.recipeId == request.recipeId);
            if (myuser == null || myrecipe == null || myfavorites != null)
            {
                await RemoveFavoriteById(myfavorites.favoriteId);
                return NotFound("favorite removed");
            }
                
            var newfavorite = new FavoritesModel
            {
                userId = request.userId,
                recipeId = request.recipeId,
            };

            await _context.Favorites.AddAsync(newfavorite);
            await _context.SaveChangesAsync();

            return Ok("favorite added");
        }

        [HttpPost("removefavorite")]
        public async Task<ActionResult> RemoveFavoriteById(int id)
        {
            var favoritetoDelete = await _context.Favorites.FindAsync(id);
            if (favoritetoDelete == null) return BadRequest("favorite not found");

            _context.Favorites.Remove(favoritetoDelete);
            await _context.SaveChangesAsync();

            return Ok("favorite removed");
        }

        [HttpGet("getfavorites")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAllFavoritesById(int id)
        {
            var myfavorite = await _context.Favorites
                         .Where(f => f.userId == id)
                         .Select(f => new
                         {
                             f.recipeId,
                             f.Recipe.Title,
                             f.Recipe.Description,
                             f.Recipe.Ingredients,
                             f.Recipe.Rating,
                             f.Recipe.prepTime,
                             f.Recipe.Portions,
                             f.Recipe.User.userName
                         })
                         .ToListAsync();

            return Ok(myfavorite);
        }

        [HttpPost("contactus")]
        public void ContactUs(ContactUsDTO request)
        {
            //stuur email met ingevulde info.
            emailCreator.RecieveEmailContactUs(request.name, request.email, request.subject, request.body);
        }
    }
}
