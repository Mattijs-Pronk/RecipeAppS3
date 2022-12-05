using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.RecipeDTO_s;
using Back_end_API.BusinessLogic.UserDTO_s;
using Back_end_API.Controllers;
using Back_end_API.Data;
using Back_end_API.Models;
using Back_end_API.SignalRHubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class AdminControllerTests
    {
        public AdminController adminController = null!;

        public static RecipeAppContext _context = null!;
        public readonly IHubContext<AdminHub> _hub = null!;

        public AdminControllerTests()
        {
            SeedDb();
        }

        VerifyInfo verifyInfo = new VerifyInfo();
        private string verifyAccountToken = null!;
        private string passwordResetToken = null!;

        [Fact]
        private void SeedDb()
        {
            var options = new DbContextOptionsBuilder<RecipeAppContext>()
                .UseInMemoryDatabase(databaseName: "AdminControllerTestDb")
                .Options;

            _context = new RecipeAppContext(options);
            _context.Database.EnsureDeleted();
            adminController = new AdminController(_context, _hub);
            

            verifyInfo.CreatePasswordHash("test123", out byte[] passwordhash, out byte[] passwordsalt);
            passwordResetToken = verifyInfo.CreateRandomToken();
            verifyAccountToken = verifyInfo.CreateRandomToken();
            var users = new List<UserModel>
            {
                new UserModel
                {
                    userId = 200,
                    userName = "pak",
                    Email = "pak@example.com",
                    adress = "test str.15",
                    phone = "329029034",
                    passwordHash = passwordhash,
                    passwordSalt = passwordsalt,
                    isAdmin = false,
                    activeSince = DateTime.Now,
                    passwordResetToken = passwordResetToken,
                    passwordResetTokenExpires = DateTime.Now.AddMinutes(15),
                    activateAccountToken = verifyAccountToken,
                    activateAccountTokenExpires = DateTime.Now.AddDays(5)
                },

                new UserModel
                {
                    userId = 210,
                    userName = "tak",
                    Email = "tak@example.com",
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
                },
            };

            var recipe = new RecipeModel
            {
                recipeId = 130,
                Title = "lekkah",
                Ingredients = "pleuris zooi",
                Description = "je gwn derin pleuren",
                prepTime = 5,
                Portions = 1,
                imageFile = new byte[0],
                Status = "Accepted",
                userId = 1,
            };

            _context.Recipes.Add(recipe);
            _context.Users.AddRange(users);
            _context.SaveChanges();
        }

        //[Fact]
        //public async Task Test_AcceptRecipeRequest_OkResult()
        //{
        //    //arrange
        //    SeedDb();
        //    int id = 130;
        //    var adminController = new AdminController(_context, _hub);


        //    //act
        //    var user = await adminController.AcceptRecipeRequest(id);
        //    var result = (ObjectResult)user;
        //    await _context.Database.EnsureDeletedAsync();

        //    //assert
        //    Assert.NotNull(result);
        //    Assert.Equal(200, result.StatusCode);
        //}

        [Fact]
        public async Task Test_AcceptRecipeRequest_BadRequestResult()
        {
            //arrange
            int id = 0;

            //act
            var user = await adminController.AcceptRecipeRequest(id);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_EditRecipeRequest_OkResult()
        {
            //arrange
            var myrecipe = new EditRecipeDTO
            {
                recipeId = 130,
                Title = "hoi",
                Ingredients = "dazal",
                Description = "kengebeuren",
                prepTime = 110,
                Portions = 1,
            };

            //act
            var user = await adminController.EditRecipeRequest(myrecipe);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_EditRecipeRequest_BadRequestResult()
        {
            //arrange
            var myrecipe = new EditRecipeDTO
            {
                recipeId = 0,
                Title = "hoi",
                Ingredients = "dazal",
                Description = "kengebeuren",
                prepTime = 110,
                Portions = 1,
            };

            //act
            var user = await adminController.EditRecipeRequest(myrecipe);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        //[Fact]
        //public async Task Test_DeclineRecipeRequest_OkResult()
        //{
        //    arrange
        //    SeedDb();
        //    int id = 130;
        //    var adminController = new AdminController(_context, _hub);


        //    act
        //    var user = await adminController.DeclineRecipeRequest(id);
        //    var result = (ObjectResult)user;
        //    await _context.Database.EnsureDeletedAsync();

        //    assert
        //    Assert.NotNull(result);
        //    Assert.Equal(200, result.StatusCode);
        //}

        [Fact]
        public async Task Test_DeclineRecipeRequest_BadRequestResult()
        {
            //arrange
            int id = 0;

            //act
            var user = await adminController.DeclineRecipeRequest(id);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_DeleteUser_OkResult()
        {
            //arrange
            int id = 210;

            //act
            var user = await adminController.DeleteUserById(id);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_DeleteUser_BadRequestResult()
        {
            //arrange
            int id = 0;

            //act
            var user = await adminController.DeleteUserById(id);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_EditUser_OkResult()
        {
            //arrange
            var myuser = new UserDTO
            {
                userId = 210,
                userName = "piet",
                email = "piet@example.com",
                adress = "",
                phone = "",
                password = "peitjuh",
                isAdmin = false,
            };

            //act
            var user = await adminController.EditUser(myuser);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_EditUser_BadRequestResult()
        {
            //arrange
            var myuser = new UserDTO
            {
                userId = 0,
                userName = "piet",
                email = "piet@example.com",
                adress = "",
                phone = "",
                password = "peitjuh",
                isAdmin = false,
            };

            //act
            var user = await adminController.EditUser(myuser);
            var result = (ObjectResult)user;

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
