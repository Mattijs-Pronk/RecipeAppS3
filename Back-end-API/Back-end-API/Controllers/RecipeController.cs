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


        [HttpGet]
        public async Task<ActionResult> GetAllRecipes()
        {
            var result = await _context.Recipes
                         .Where(result => result.Active == true)
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


        [HttpGet("id")]
        [ProducesResponseType(typeof(RecipeModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetRecipeById(int id)
        {
            var result = await _context.Recipes
                         .Where(result => result.recipeId == id)
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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<RecipeModel>> CreateRecipe(CreateRecipeDTO recipe)
        {
            var result = await _context.Users.FindAsync(recipe.userId);
            if (result == null)
                return NotFound();

            var newrecipe = new RecipeModel
            {
                Title = recipe.Title,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients,
                prepTime = recipe.prepTime,
                Portions = recipe.Portions,
                Rating = recipe.Rating,
                Active = recipe.Active,
                User = result
            };

            await _context.Recipes.AddAsync(newrecipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipeById), new { id = newrecipe.recipeId }, newrecipe);
        }


        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteRecipeById(int id)
        {
            var recipetoDelete = await _context.Recipes.FindAsync(id);
            if (recipetoDelete == null) return BadRequest();

            _context.Recipes.Remove(recipetoDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
