using Back_end_API.BuisnessLogic;
using Back_end_API.DAL;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_end_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        public readonly RecipeAppContext _context;

        RecipeContainer recipeContainer = new RecipeContainer();

        public RecipeController(RecipeAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeModel>> GetAllRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(RecipeModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            return recipe == null ? NotFound() : Ok(recipe);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRecipe(RecipeModel recipe)
        {
            //Recipe recipe1 = new Recipe();
            //recipe1.recipeId = recipe.recipeId;
            //recipe1.Title = recipe.Title;
            //recipe1.Description = recipe.Description;
            //recipe1.Ingredients = recipe.Ingredients;
            //recipe1.PrepTime = (int)recipe.prepTime;
            //recipe1.Rating = recipe.Rating;
            //recipe1.Active = recipe.Active;
            //recipe1.Portions = (int)recipe.Portions;
            //recipe1.userId = (int)recipe.userId;


            //await recipeContainer.AddOneReservation(recipe1);

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.recipeId }, recipe);
        }

    }
}
