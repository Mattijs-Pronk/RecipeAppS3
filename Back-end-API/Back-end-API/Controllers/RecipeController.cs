using Back_end_API.BusinessLogic;
using Back_end_API.Data;
using Back_end_API.Models;
using Back_end_API.SignalRHubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;

namespace Back_end_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        public readonly RecipeAppContext _context;
        public readonly IHubContext<AdminHub> _hub;

        ImageConverter imgConverter = new ImageConverter();

        public RecipeController(RecipeAppContext context, IHubContext<AdminHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        /// <summary>
        /// Methode om alle geaccepteerde recepten op te halen.
        /// </summary>
        /// <returns>Ok wanneer alle recepten zijn verstuurd.</returns>
        [HttpGet("getallaccepted")]
        public async Task<ActionResult> GetAllAcceptedRecipes()
        { 
            var myrecipe = await _context.Recipes
                         .Where(r => r.Status == RecipeModel.status.Accepted.ToString())
                         .Select(r => new
                         {
                             r.recipeId,
                             r.Title,
                             r.Description,
                             r.Ingredients,
                             r.prepTime,
                             r.Portions,
                             createDate = r.createDate.ToString("dd-MM-yyyy"),
                             imageBase64 = Convert.ToBase64String(r.imageFile),
                             r.User.userName
                         })
                         .ToListAsync();

            return Ok(myrecipe);
        }

        /// <summary>
        /// Methode om alle geaccepteerde recepten op te halen.
        /// </summary>
        /// <returns>Ok wanneer alle recepten zijn verstuurd.</returns>
        [HttpGet("getallonhold")]
        public async Task<ActionResult> GetAllOnHoldRecipes()
        {
            var myrecipe = await _context.Recipes
                         .Where(r => r.Status == RecipeModel.status.Onhold.ToString())
                         .Select(r => new
                         {
                             r.recipeId,
                             r.Title,
                             r.Description,
                             r.Ingredients,
                             r.prepTime,
                             r.Portions,
                             createDate = r.createDate.ToString("dd-MM-yyyy"),
                             imageBase64 = Convert.ToBase64String(r.imageFile),
                             r.User.userName
                         })
                         .ToListAsync();

            return Ok(myrecipe);
        }

        /// <summary>
        /// Methode om alle afgewezen recepten op te halen.
        /// </summary>
        /// <returns>Ok wanneer alle recepten zijn verstuurd.</returns>
        [HttpGet("getalldeclined")]
        public async Task<ActionResult> GetAllDeclinedRecipes()
        {
            var myrecipe = await _context.Recipes
                         .Where(r => r.Status == RecipeModel.status.Declined.ToString())
                         .Select(r => new
                         {
                             r.recipeId,
                             r.Title,
                             r.Description,
                             r.Ingredients,
                             r.prepTime,
                             r.Portions,
                             createDate = r.createDate.ToString("dd-MM-yyyy"),
                             imageBase64 = Convert.ToBase64String(r.imageFile),
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
                             r.prepTime,
                             r.Portions,
                             createDate = r.createDate.ToString("dd-MM-yyyy"),
                             imageBase64 = Convert.ToBase64String(r.imageFile),
                             r.User.userName
                         })
                         .ToArrayAsync();

            return Ok(myrecipe);
        }

        /// <summary>
        /// Methode om een recept aan te maken.
        /// </summary>
        /// <param name="request">verzameling van title, ingredients, description, preptime, portions en userid van ingevulde front-end.</param>
        /// <returns>Ok wanneer user is gevonden en recept is teogevoegd, Bad request wanneer user niet is gevonden.</returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateRecipe([FromForm]RecipeDTO request)
        {
            var myuser = await _context.Users.FindAsync(request.userId);
            if (myuser == null)
                return BadRequest("user not found");

            var fileByte = imgConverter.UploadImage(request.imageFile);

            var newrecipe = new RecipeModel
            {
                Title = request.Title,
                Description = request.Description,
                Ingredients = request.Ingredients,
                prepTime = request.prepTime,
                Portions = request.Portions,
                imageFile = fileByte,
                createDate = DateTime.Now,
                Status = RecipeModel.status.Onhold.ToString(),
                User = myuser
            };

            await _context.Recipes.AddAsync(newrecipe);
            await _context.SaveChangesAsync();

            //recipe ophalen want anders is er geen image beschikibaar.
            var newrecipeasync = await GetRecipeById(newrecipe.recipeId);
            await _hub.Clients.All.SendAsync("ReceiveRecipe", newrecipeasync);
            await _hub.Clients.All.SendAsync("ReceiveMessage", myuser.userName + " has added a new recipe, Title: " + newrecipe.Title);

            return Ok("recipe added");
        }
    }
}
