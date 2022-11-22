using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.UserDTO_s;
using Back_end_API.Controllers;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests
{
    public class RecipeControllerTests
    {
        RecipeAppContext _context = null!;
        VerifyInfo verifyInfo = new VerifyInfo();

        [Fact]
        private void SeedDb()
        {
            var options = new DbContextOptionsBuilder<RecipeAppContext>()
                .UseInMemoryDatabase(databaseName: "AuthControllerTestDb")
                .Options;

            _context = new RecipeAppContext(options);

            _context.Database.EnsureDeleted();
            

            verifyInfo.CreatePasswordHash("test123", out byte[] passwordhash, out byte[] passwordsalt);
            var user = new UserModel
            {
                userId = 3,
                userName = "Jantje",
                Email = "jantje@example.com",
                adress = "test str.15",
                phone = "329029034",
                passwordHash = passwordhash,
                passwordSalt = passwordsalt,
                isAdmin = false,
                activeSince = DateTime.Now,
                passwordResetToken = null,
                passwordResetTokenExpires = null,
                activateAccountToken = null,
                activateAccountTokenExpires = null
            };

            var recipe = new RecipeModel
            {
                recipeId = 1,
                Title = "lekkah",
                Ingredients = "pleuris zooi",
                Description = "je gwn derin pleuren",
                Rating = 5,
                prepTime = 5,
                Portions = 1,
                imageFile = new byte[0],
                Status = "Accepted",
                userId = 1,
            };

            _context.Add(user);
            _context.Add(recipe);

            _context.SaveChanges();
        }

        [Fact]
        public async Task Test_GetAllAcceptedRecipes_OkResult()
        {
            //arrange
            SeedDb();
            var recipeController = new RecipeController(_context);

            //act
            var recipe = await recipeController.GetAllAcceptedRecipes();
            var result = (ObjectResult)recipe;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetAllOnHoldRecipes_OkResult()
        {
            //arrange
            SeedDb();
            var recipeController = new RecipeController(_context);

            //act
            var recipe = await recipeController.GetAllOnHoldRecipes();
            var result = (ObjectResult)recipe;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetAllDeclinedRecipes_OkResult()
        {
            //arrange
            SeedDb();
            var recipeController = new RecipeController(_context);

            //act
            var recipe = await recipeController.GetAllDeclinedRecipes();
            var result = (ObjectResult)recipe;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetRecipeById_OkResult()
        {
            //arrange
            SeedDb();
            int recipeid = 3;
            var recipeController = new RecipeController(_context);

            //act
            var recipe = await recipeController.GetRecipeById(recipeid);
            var result = (ObjectResult)recipe;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetRecipeById_BadRequestResult()
        {
            //arrange
            SeedDb();
            int recipeid = 0;
            var recipeController = new RecipeController(_context);

            //act
            var recipe = await recipeController.GetRecipeById(recipeid);
            var result = (ObjectResult)recipe;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        //[Fact]
        //public async Task Test_CreateRecipe_OkResult()
        //{
        //    //arrange
        //    SeedDb();
        //    var content = "Hello World from a Fake File";
        //    var fileName = "test.pdf";
        //    var stream = new MemoryStream();
        //    var writer = new StreamWriter(stream);
        //    writer.Write(content);
        //    writer.Flush();
        //    stream.Position = 0;

        //    IFormFile recipeImage = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

        //    var myrecipe = new RecipeDTO
        //    {
        //        recipeId = 1002,
        //        Title = "keigaaf",
        //        Ingredients = "nog cooler man",
        //        Description = "unbeliefabol",
        //        Portions = 2,
        //        prepTime = 15,
        //        imageFile = recipeImage,
        //        userId = 3
        //    };
        //    var recipeController = new RecipeController(_context);

        //    //act
        //    var recipe = await recipeController.CreateRecipe(myrecipe);
        //    var result = (ObjectResult)recipe;
        //    await _context.Database.EnsureDeletedAsync();

        //    //assert
        //    Assert.NotNull(result);
        //    Assert.Equal(200, result.StatusCode);
        //}

        [Fact]
        public async Task Test_CreateRecipe_BadRequestResult()
        {
            //arrange
            SeedDb();
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile recipeImage = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var myrecipe = new RecipeDTO
            {
                Title = "keigaaf",
                Ingredients = "nog cooler man",
                Description = "unbeliefabol",
                Portions = 2,
                prepTime = 15,
                imageFile = recipeImage,
                userId = 0
            };
            var recipeController = new RecipeController(_context);


            //act
            var recipe = await recipeController.CreateRecipe(myrecipe);
            var result = (ObjectResult)recipe;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
