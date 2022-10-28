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

        public UserController(RecipeAppContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUserById(int id)
        {
            var myuser = await _context.Users
            .Where(myuser => myuser.userId == id)
                         .Select(myuser => new
                         {
                             myuser.userId,
                             myuser.userName,
                             myuser.Email,
                             myuser.adress,
                             myuser.phone,
                             myuser.activeSince
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateUser(UserModel user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new {id = user.userId}, user);
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateUserById(int id, UserModel user)
        {
            if(id != user.userId) return BadRequest();

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUserById(int id)
        {
            var usertoDelete = await _context.Users.FindAsync(id);
            if(usertoDelete == null) return BadRequest();

            _context.Users.Remove(usertoDelete);
            await _context.SaveChangesAsync();

            return NoContent();
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
                myuser.userName = request.userName;
                if(request.adress == "") { myuser.adress = myuser.adress; }
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
            var result = await _context.Recipes
                         .Where(result => result.userId == id)
                         .Select(result => new
                         {
                             result.recipeId,
                             result.Title,
                             result.Description,
                             result.Ingredients,
                             result.Rating,
                             result.prepTime,
                             result.Portions,
                             result.User.userName
                         })
                         .ToListAsync();

            return Ok(result);
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
            var result = await _context.Favorites
                         .Where(result => result.userId == id)
                         .Select(result => new
                         {
                             result.recipeId,
                             result.Recipe.Title,
                             result.Recipe.Description,
                             result.Recipe.Ingredients,
                             result.Recipe.Rating,
                             result.Recipe.prepTime,
                             result.Recipe.Portions,
                             result.Recipe.User.userName
                         })
                         .ToListAsync();

            return Ok(result);
        }
    }
}
