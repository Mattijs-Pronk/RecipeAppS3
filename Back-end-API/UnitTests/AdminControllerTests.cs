using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.RecipeDTO_s;
using Back_end_API.BusinessLogic.UserDTO_s;
using Back_end_API.Controllers;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class AdminControllerTestss
    {
        public static RecipeAppContext _context = null!;

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
                Rating = 5,
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

        [Fact]
        public async Task Test_AcceptRecipeRequest_OkResult()
        {
            //arrange
            SeedDb();
            int id = 130;
            var adminController = new AdminController(_context);


            //act
            var user = await adminController.AcceptRecipeRequest(id);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_AcceptRecipeRequest_BadRequestResult()
        {
            //arrange
            SeedDb();
            int id = 0;
            var adminController = new AdminController(_context);


            //act
            var user = await adminController.AcceptRecipeRequest(id);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_EditRecipeRequest_OkResult()
        {
            //arrange
            SeedDb();
            var myrecipe = new EditRecipeDTO
            {
                recipeId = 130,
                Title = "hoi",
                Ingredients = "dazal",
                Description = "kengebeuren",
                prepTime = 110,
                Portions = 1,
            };

            var adminController = new AdminController(_context);


            //act
            var user = await adminController.EditRecipeRequest(myrecipe);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_EditRecipeRequest_BadRequestResult()
        {
            //arrange
            SeedDb();
            var myrecipe = new EditRecipeDTO
            {
                recipeId = 0,
                Title = "hoi",
                Ingredients = "dazal",
                Description = "kengebeuren",
                prepTime = 110,
                Portions = 1,
            };

            var adminController = new AdminController(_context);


            //act
            var user = await adminController.EditRecipeRequest(myrecipe);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_DeclineRecipeRequest_OkResult()
        {
            //arrange
            SeedDb();
            int id = 130;
            var adminController = new AdminController(_context);


            //act
            var user = await adminController.DeclineRecipeRequest(id);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_DeclineRecipeRequest_BadRequestResult()
        {
            //arrange
            SeedDb();
            int id = 0;
            var adminController = new AdminController(_context);


            //act
            var user = await adminController.DeclineRecipeRequest(id);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_DeleteUser_OkResult()
        {
            //arrange
            SeedDb();
            int id = 210;
            var adminController = new AdminController(_context);


            //act
            var user = await adminController.DeleteUserById(id);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_DeleteUser_BadRequestResult()
        {
            //arrange
            SeedDb();
            int id = 0;
            var adminController = new AdminController(_context);


            //act
            var user = await adminController.DeleteUserById(id);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_EditUser_OkResult()
        {
            //arrange
            SeedDb();
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

            var adminController = new AdminController(_context);


            //act
            var user = await adminController.EditUser(myuser);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_EditUser_BadRequestResult()
        {
            //arrange
            SeedDb();
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

            var adminController = new AdminController(_context);


            //act
            var user = await adminController.EditUser(myuser);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
