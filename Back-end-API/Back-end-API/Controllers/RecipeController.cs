﻿using Back_end_API.BusinessLogic;
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
        public async Task<ActionResult> GetAllRecipes()
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
                             r.User.userName
                         })
                         .ToListAsync();

            return Ok(myrecipe);
        }

        /// <summary>
        /// Methode om een recept an te maken.
        /// </summary>
        /// <param name="request">verzameling van title, ingredients, description, preptime, portions en userid van ingevulde front-end.</param>
        /// <returns>Ok wanneer user is gevonden en recept is teogevoegd, Bad request wanneer user niet is gevonden.</returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<RecipeModel>> CreateRecipe([FromForm]CreateRecipeDTO request)
        {
            var myuser = await _context.Users.FindAsync(request.userId);
            if (myuser == null)
                return NotFound();

            var filename = await UploadImage(request.imageFile);

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
        [HttpPost("uploadimg")]
        public async Task<string> UploadImage(IFormFile request)
        {
            if(request.Length > 0)
            {
                string path = _env.WebRootPath + "\\uploads\\";
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

        //nog over zetten naar andere functie (apparte class).
        [HttpGet("{fileName}")]
        public async Task<ActionResult> GetImage(string fileName)
        {
            string path = _env.WebRootPath + "\\uploads\\";
            var filepath = path + fileName;
            if (System.IO.File.Exists(filepath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filepath);
                return File(b, "image/png");
            }
            return null;
        }

        [HttpGet("{fileName2}")]
        public async Task<string> GetImage2(string fileName)
        {
            string path = _env.WebRootPath + "\\uploads\\";
            var filepath = path + fileName;
            if (System.IO.File.Exists(filepath))
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(filepath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                return base64ImageRepresentation;
            }
            return null;
        }

        [HttpGet("testgetrecipes")]
        public async Task<ActionResult> Test(int id)
        {
            var myrecipe1 = await _context.Recipes.FindAsync(id);
            if (myrecipe1 == null)
                return NotFound();

            var imageFile = await GetImage2(myrecipe1.imageName);

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
                             imageFile,
                             r.User.userName
                         })
                         .ToListAsync();

            return Ok(myrecipe);
        }
    }
}
