using Back_end_API.BusinessLogic;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections;
using System.Linq;

namespace Back_end_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        public readonly RecipeAppContext _context;
        private readonly IWebHostEnvironment _env;

        public RecipeController(RecipeAppContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        /// <summary>
        /// Methode om alle recepten op te halen.
        /// </summary>
        /// <returns>Ok wanneer alle recepten zijn verstuurd.</returns>
        [HttpGet("getall")]
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
                             r.Rating,
                             r.prepTime,
                             r.Portions,
                             r.imageName,
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
                             r.Rating,
                             r.prepTime,
                             r.Portions,
                             r.imageName,
                             r.User.userName
                         })
                         .ToArrayAsync();

            return Ok(myrecipe);
        }

        /// <summary>
        /// Methode om een recept an te maken.
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

            var filename = UploadImage(request.imageFile);

            var newrecipe = new RecipeModel
            {
                Title = request.Title,
                Description = request.Description,
                Ingredients = request.Ingredients,
                prepTime = request.prepTime,
                Portions = request.Portions,
                Rating = 0,
                imageName = filename,
                Status = RecipeModel.status.Onhold.ToString(),
                User = myuser
            };

            await _context.Recipes.AddAsync(newrecipe);
            await _context.SaveChangesAsync();

            return Ok("recipe added");
        }

        //nog over zetten naar andere functie (apparte class).
        [NonAction]
        public string UploadImage(IFormFile request)
        {
            if(request.Length > 0)
            {
                string path = _env.WebRootPath + "\\recipeImg\\";
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileName = DateTime.Now.ToString("yymmssfff") + request.FileName;
                using (FileStream fileStream = System.IO.File.Create(path + fileName))
                {
                    request.CopyTo(fileStream);
                    fileStream.Flush();
                    return fileName;
                }
            }
            else { return "upload failed"; }
        }
    }
}
