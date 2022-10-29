using Back_end_API.BusinessLogic;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;

namespace Back_end_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        public readonly RecipeAppContext _context;

        public RecipeController(RecipeAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Methode om alle recepten op te halen.
        /// </summary>
        /// <returns>Ok wanneer alle recepten zijn verstuurd.</returns>
        [HttpGet("getall")]
        public async Task<ActionResult> GetAllRecipes()
        {
            var myrecipe = await _context.Recipes
                         .Where(r => r.Active == true)
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

        /// <summary>
        /// Meethode om 3 random recepten op te halen.
        /// </summary>
        /// <returns>Ok wanneer 3 recepten zijn verstuurd.</returns>
        [HttpGet("getrandom")]
        public async Task<ActionResult> GetRandomRecipes()
        {
            var myrecipe = await _context.Recipes.OrderBy(x => Guid.NewGuid()).Take(3)
                         .Where(r => r.Active == true)
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

        /// <summary>
        /// Methode om een recept op te halen.
        /// </summary>
        /// <param name="id">recept id van ingevulde front-end.</param>
        /// <returns>Ok wanneer recept is opgestuurd.</returns>
        [HttpGet("getrecipe")]
        [ProducesResponseType(typeof(RecipeModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetRecipeById(int id)
        {
            var myrecipe = await _context.Recipes
                         .Where(r => r.recipeId == id)
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

        /// <summary>
        /// Methode om een recept an te maken.
        /// </summary>
        /// <param name="recipe">verzameling van title, ingredients, description, preptime, portions en userid van ingevulde front-end.</param>
        /// <returns>Ok wanneer user is gevonden en recept is teogevoegd, Bad request wanneer user niet is gevonden.</returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<RecipeModel>> CreateRecipe(CreateRecipeDTO request)
        {
            var myuser = await _context.Users.FindAsync(request.userId);
            if (myuser == null)
                return NotFound();

            var newrecipe = new RecipeModel
            {
                Title = request.Title,
                Description = request.Description,
                Ingredients = request.Ingredients,
                prepTime = request.prepTime,
                Portions = request.Portions,
                Rating = 0,
                Active = true, /*recipe.Active,*/
                User = myuser
            };

            await _context.Recipes.AddAsync(newrecipe);
            await _context.SaveChangesAsync();

            return Ok("recipe added");
        }
    }
}
