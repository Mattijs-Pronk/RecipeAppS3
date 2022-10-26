using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.FavoriteDTO_s;
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
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
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
            if (myuser == null || myrecipe == null)
                return NotFound("user or recipe doesn't exist");

            var newfavorite = new FavoritesModel
            {
                userId = request.userId,
                recipeId = request.recipeId,
            };

            await _context.Favorites.AddAsync(newfavorite);
            await _context.SaveChangesAsync();

            return Ok("favorite added");
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
