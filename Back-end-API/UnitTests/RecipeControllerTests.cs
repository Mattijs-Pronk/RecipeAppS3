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
        RecipeController recipeController;
        VerifyInfo verifyInfo = new VerifyInfo();

        public RecipeControllerTests()
        {
            SeedDb();
        }

        [Fact]
        private void SeedDb()
        {
            var options = new DbContextOptionsBuilder<RecipeAppContext>()
                .UseInMemoryDatabase(databaseName: "AuthControllerTestDb")
                .Options;

            var context = new RecipeAppContext(options);

            context.Database.EnsureDeleted();

            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            recipeController = new RecipeController(context, mockEnvironment.Object);
            

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
                imageName = "koekje.png",
                Status = "Accepted",
                userId = 1,
            };

            context.Add(user);
            context.Add(recipe);

            context.SaveChanges();
        }

        [Fact]
        public async Task Test_GetAllRecipes_OkResult()
        {
            //arrange


            //act
            var recipe = await recipeController.GetAllAcceptedRecipes();
            var result = (ObjectResult)recipe;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        //[Fact]
        //public async Task Test_Register_BadRequestResult()
        //{
        //    //arrange


        //    //act
        //    var recipe = await recipeController.GetAllRecipes();
        //    var result = (ObjectResult)recipe;


        //    //assert
        //    Assert.NotNull(result);
        //    Assert.Equal(400, result.StatusCode);
        //}

        [Fact]
        public async Task Test_GetRecipeById_OkResult()
        {
            //arrange
            int recipeid = 3;

            //act
            var recipe = await recipeController.GetRecipeById(recipeid);
            var result = (ObjectResult)recipe;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetRecipeById_BadRequestResult()
        {
            //arrange
            int recipeid = 0;

            //act
            var recipe = await recipeController.GetRecipeById(recipeid);
            var result = (ObjectResult)recipe;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_CreateRecipe_OkResult()
        {
            //arrange

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
                userId = 3
            };

            //act
            var recipe = await recipeController.CreateRecipe(myrecipe);
            var result = (ObjectResult)recipe;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_CreateRecipe_BadRequestResult()
        {
            //arrange
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


            //act
            var recipe = await recipeController.CreateRecipe(myrecipe);
            var result = (ObjectResult)recipe;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Test_UploadImage_BadRequestResult()
        {
            //arrange
            var fileName = "test.pdf";
            var stream = new MemoryStream();

            IFormFile recipeImage = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);


            //act
            var recipe = recipeController.UploadImage(recipeImage);


            //assert
            Assert.NotNull(recipe);
            Assert.Equal("upload failed", recipe);
        }
    }
}
