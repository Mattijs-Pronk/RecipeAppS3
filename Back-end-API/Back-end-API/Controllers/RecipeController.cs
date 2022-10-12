﻿using Back_end_API.BusinessLogic;
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

        public RecipeController(RecipeAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeModel>> GetAllRecipes()
            => await _context.Recipes.ToListAsync();


        [HttpGet("id")]
        [ProducesResponseType(typeof(RecipeModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var result = await _context.Recipes
                .Where(c => c.recipeId == id)
                .Include(c => c.User)
                .ToListAsync();

            var recipe = await _context.Recipes.FindAsync(id);
            return recipe == null ? NotFound() : Ok(recipe);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<List<RecipeModel>>> CreateRecipe(RecipeDTO recipe)
        {
            var result = _context.Users.FindAsync(recipe.userId);
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
                User = await result
            };

            await _context.Recipes.AddAsync(newrecipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipeById), new { id = newrecipe.recipeId }, newrecipe);
        }

    }
}
