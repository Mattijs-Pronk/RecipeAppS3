using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.RecipeDTO_s;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_end_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        public readonly RecipeAppContext _context;

        ImageConverter imgConverter = new ImageConverter();

        public AdminController(RecipeAppContext context)
        {
            _context = context;
        }

        [HttpPut("acceptrecipe")]
        public async Task<ActionResult> AcceptRecipeRequest(int id)
        {
            var myrecipe = await _context.Recipes.FindAsync(id);
            if(myrecipe != null)
            {
                myrecipe.Status = RecipeModel.status.Accepted.ToString();

                await _context.SaveChangesAsync();
                return Ok("request accepted");
            }

            return BadRequest("recipe not found");
        }

        [HttpPut("editrecipe")]
        public async Task<ActionResult> EditRecipeRequest(EditRecipeDTO request)
        {
            var myrecipe = await _context.Recipes.FindAsync(request.recipeId);
            if (myrecipe != null)
            {
                myrecipe.Title = request.Title;
                myrecipe.Ingredients = request.Ingredients;
                myrecipe.Description = request.Description;
                myrecipe.Portions = request.Portions;
                myrecipe.prepTime = request.prepTime;
                myrecipe.Status = RecipeModel.status.Accepted.ToString();

                await _context.SaveChangesAsync();
                return Ok("recipe edited");
            }

            return BadRequest("recipe not found");
        }

        [HttpPut("declinerecipe")]
        public async Task<ActionResult> DeclineRecipeRequest(int id)
        {
            var myrecipe = await _context.Recipes.FindAsync(id);
            if (myrecipe != null)
            {
                myrecipe.Status = RecipeModel.status.Declined.ToString();

                await _context.SaveChangesAsync();
                return Ok("request declined");
            }

            return BadRequest("recipe not found");
        }

        [HttpPost("deleteuser")]
        public async Task<ActionResult> DeleteUserById(int id)
        {
            var myuser = await _context.Users.FindAsync(id);
            if (myuser != null && myuser.isAdmin == false)
            {
                var myfavorites = _context.Favorites.Where(f => f.userId == id)
                .ToList();

                _context.Users.Remove(myuser);
                _context.Favorites.RemoveRange(myfavorites);
                await _context.SaveChangesAsync();
                return Ok("user deleted");
            }
            return BadRequest("user not found or user is admin");
        }
    }
}
