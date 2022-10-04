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
        {
            return await _context.Recipes.ToListAsync();
        }
    }
}
