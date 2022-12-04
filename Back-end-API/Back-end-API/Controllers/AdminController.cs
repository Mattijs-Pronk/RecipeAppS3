using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.RecipeDTO_s;
using Back_end_API.BusinessLogic.UserDTO_s;
using Back_end_API.Data;
using Back_end_API.Models;
using Back_end_API.SignalRHubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Back_end_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        public readonly RecipeAppContext _context;
        public readonly IHubContext<AdminHub> _hub;

        VerifyInfo verify = new VerifyInfo();

        public AdminController(RecipeAppContext context, IHubContext<AdminHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        /// <summary>
        /// Methode om recept te accepteren.
        /// </summary>
        /// <param name="id">id van recept.</param>
        /// <returns>Ok wanneer recept is gevonden en aangepast.</returns>
        [HttpPut("acceptrecipe")]
        public async Task<ActionResult> AcceptRecipeRequest(int id)
        {
            var myrecipe = await _context.Recipes.FindAsync(id);
            if(myrecipe != null)
            {
                myrecipe.Status = RecipeModel.status.Accepted.ToString();

                await _context.SaveChangesAsync();

                await _hub.Clients.All.SendAsync("RemoveRecipe", id);
                return Ok("request accepted");
            }

            return BadRequest("recipe not found");
        }

        /// <summary>
        /// Methode om een recept aan te passen.
        /// </summary>
        /// <param name="request">Verzameling van recipeid, title, ingredients, description, preptime, potions</param>
        /// <returns>Ok wanneer recept is gevonden en aangepast.</returns>
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

        /// <summary>
        /// Methode om een recept te declinen.
        /// </summary>
        /// <param name="id">recipeid</param>
        /// <returns>Ok wanneer recept is gevonden en aangepast.</returns>
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

        /// <summary>
        /// Methode om een user te verwijderen.
        /// </summary>
        /// <param name="id">userid.</param>
        /// <returns>Ok wanneer user is gevonden en user is geen admin en user is verwijderd.</returns>
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

        /// <summary>
        /// Methode om een user aan te passen.
        /// </summary>
        /// <param name="request">Verzameling van userid, username, email, adress, phone, password, isadmin</param>
        /// <returns>Ok wanneer user is gevonden en geen admin is en is aangepast.</returns>
        [HttpPut("edituser")]
        public async Task<ActionResult> EditUser(UserDTO request)
        {
            var myuser = await _context.Users.FindAsync(request.userId);
            if (myuser != null && myuser.isAdmin == false)
            {
                if(request.password != "") 
                { 
                    verify.CreatePasswordHash(request.password, out byte[] passwordhash, out byte[] passwordsalt);
                    myuser.passwordHash = passwordhash;
                    myuser.passwordSalt = passwordsalt;
                }

                myuser.userName = request.userName;
                myuser.Email = request.email;
                myuser.adress = request.adress;
                myuser.phone = request.phone; 
                myuser.isAdmin = request.isAdmin;

                await _context.SaveChangesAsync();
                return Ok("user edited");
            }
            return BadRequest("user not found or user is admin");
        }
    }
}
