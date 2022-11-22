using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.FavoriteDTO_s;
using Back_end_API.BusinessLogic.UserDTO_s;
using Back_end_API.Controllers;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class UserControllerTests
    {
        RecipeAppContext _context = null!;
        VerifyInfo verifyInfo = new VerifyInfo();


        [Fact]
        private void SeedDb()
        {
            var options = new DbContextOptionsBuilder<RecipeAppContext>()
                .UseInMemoryDatabase(databaseName: "UserControllerTestDb")
                .Options;

            _context = new RecipeAppContext(options);

            _context.Database.EnsureDeleted();

            var recipes = new List<RecipeModel>()
            {
                new RecipeModel
                {
                    recipeId = 100,
                    Title = "test",
                    Ingredients = "test",
                    Description = "test",
                    imageFile = new byte[0],
                    prepTime = 1,
                    Portions = 1,
                    Rating = 1,
                    Status = "Accepted",
                    userId = 1
                },
                new RecipeModel 
                {
                    recipeId = 150,
                    Title = "test",
                    Ingredients = "test",
                    Description = "test",
                    imageFile = new byte[0],
                    prepTime = 1,
                    Portions = 1,
                    Rating = 1,
                    Status = "Accepted",
                    userId = 1
                }
            };

            verifyInfo.CreatePasswordHash("test123", out byte[] passwordhash, out byte[] passwordsalt);
            var user = new UserModel
            {
                userId = 4,
                userName = "Peter",
                Email = "peter@example.com",
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

            var favorite = new FavoritesModel
            {
                favoriteId = 1,
                recipeId = 1,
                userId = 1
            };

            _context.Users.Add(user);
            _context.Recipes.AddRange(recipes);
            _context.Favorites.Add(favorite);
            _context.SaveChanges();
        }

        [Fact]
        public async Task Test_GetUserById_OKResult()
        {
            //arrange
            SeedDb();
            int userid = 4;
            var userController = new UserController(_context);

            //act
            var user = await userController.GetUserById(userid);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetUserById_BadRequestResult()
        {
            //arrange
            SeedDb();
            var userid = 0;
            var userController = new UserController(_context);

            //act
            var user = await userController.GetUserById(userid);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetAllUsers_OKResult()
        {
            //arrange
            SeedDb();
            var userController = new UserController(_context);

            //act
            var user = await userController.GetAllUsers();
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_ChangeProfile_OKResult()
        {
            //arrange
            SeedDb();
            var myuser = new UserDTO
            {
                userId = 4,
                userName = "Piet",
                adress = "van laan str.16",
                phone = "392034854"
            };
            var userController = new UserController(_context);

            //act
            var user = await userController.ChangeProfile(myuser);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_ChangeProfile_BadRequestResult()
        {
            //arrange
            SeedDb();
            var myuser = new UserDTO
            {
                userId = 0,
                userName = "Piet",
                adress = "van laan str.16",
                phone = "392034854"
            };
            var userController = new UserController(_context);

            //act
            var user = await userController.ChangeProfile(myuser);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_ChangePassword_OKResult()
        {
            //arrange
            SeedDb();
            var myuser = new ChangePasswordDTO
            {
                userId = 4,
                currentPassword = "test123",
                newPassword = "test1234"
            };
            var userController = new UserController(_context);

            //act
            var user = await userController.ChangePassword(myuser);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_ChangePassword_BadRequestResult()
        {
            //arrange
            SeedDb();
            var myuser = new ChangePasswordDTO
            {
                userId = 0,
                currentPassword = "test123",
                newPassword = "test1234"
            };
            var userController = new UserController(_context);

            //act
            var user = await userController.ChangePassword(myuser);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_DoubleUsernameChangeUsername_True()
        {
            //arrange
            SeedDb();
            var myuser = new ChangeUsernameDTO
            {
                currentUsername = "Piet",
                newUsername = "Peter"
            };
            var userController = new UserController(_context);

            //act
            var result = await userController.DoubleUsernameExcludeCurrentUserName(myuser);
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task Test_DoubleUsernameChangeUsername_False()
        {
            //arrange
            SeedDb();
            var myuser = new ChangeUsernameDTO
            {
                currentUsername = "Peter",
                newUsername = "Piet"
            };
            var userController = new UserController(_context);

            //act
            var result = await userController.DoubleUsernameExcludeCurrentUserName(myuser);
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.False(result.Value);
        }

        [Fact]
        public async Task Test_EamilUsernameChangeEmail_True()
        {
            //arrange
            SeedDb();
            var myuser = new ChangeEmailDTO
            {
                currentEmail = "peter",
                newEmail = "peter@example.com"
            };
            var userController = new UserController(_context);

            //act
            var result = await userController.DoubleEmailExcludeCurrentEmail(myuser);
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task Test_DoubleEmailChangeEmail_False()
        {
            //arrange
            SeedDb();
            var myuser = new ChangeEmailDTO
            {
                currentEmail = "peter@example.com",
                newEmail = "peter@example.com"
            };
            var userController = new UserController(_context);

            //act
            var result = await userController.DoubleEmailExcludeCurrentEmail(myuser);
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.False(result.Value);
        }

        [Fact]
        public async Task Test_GetUserRecipesAmountById_Int()
        {
            //arrange
            SeedDb();
            int userid = 1;
            var userController = new UserController(_context);

            //act
            var result = await userController.GetUserRecipesAmountById(userid);
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Value);
        }

        [Fact]
        public async Task Test_GetUserFavoritesAmountById_Int()
        {
            //arrange
            SeedDb();
            int userid = 1;
            var userController = new UserController(_context);

            //act
            var result = await userController.GetUserFavoritesAmountById(userid);
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Value);
        }

        [Fact]
        public async Task Test_GetUserRecipesById_OKResult()
        {
            //arrange
            SeedDb();
            int userid = 4;
            var userController = new UserController(_context);

            //act
            var user = await userController.GetUserRecipesById(userid);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetUserRecipesById_BadRequestResult()
        {
            //arrange
            SeedDb();
            int userid = 0;
            var userController = new UserController(_context);

            //act
            var user = await userController.GetUserRecipesById(userid);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetAllFavoritesById_OKResult()
        {
            //arrange
            SeedDb();
            int favoriteid = 1;
            var userController = new UserController(_context);

            //act
            var user = await userController.GetAllFavoritesById(favoriteid);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();

            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetAllFavoritesById_BadRequestResult()
        {
            //arrange
            SeedDb();
            int favoriteid = 0;
            var userController = new UserController(_context);

            //act
            var user = await userController.GetAllFavoritesById(favoriteid);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetFavoriteById_True()
        {
            //arrange
            SeedDb();
            var myfavorite = new FavoriteDTO
            {
                recipeId = 1,
                userId = 1,
            };
            var userController = new UserController(_context);

            //act
            var result = await userController.GetFavoriteById(myfavorite);
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task Test_GetFavoriteById_False()
        {
            //arrange
            SeedDb();
            var myfavorite = new FavoriteDTO
            {
                recipeId = 0,
                userId = 0,
            };
            var userController = new UserController(_context);

            //act
            var result = await userController.GetFavoriteById(myfavorite);
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.False(result.Value);
        }

        [Fact]
        public async Task Test_AddToFavorites_OkResult()
        {
            //arrange
            SeedDb();
            var myfavorite = new FavoriteDTO
            {
                recipeId = 100,
                userId = 4,
            };
            var userController = new UserController(_context);

            //act
            var user = await userController.AddToFavorites(myfavorite);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_AddToFavorites_BadRequestResult()
        {
            //arrange
            SeedDb();
            var myfavorite = new FavoriteDTO
            {
                recipeId = 1,
                userId = 1,
            };
            var userController = new UserController(_context);

            //act
            var user = await userController.AddToFavorites(myfavorite);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_RemoveFavoriteById_OkResult()
        {
            //arrange
            SeedDb();
            int favoriteid = 1;
            var userController = new UserController(_context);

            //act
            var user = await userController.RemoveFavoriteById(favoriteid);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]

        public async Task Test_RemoveFavoriteById_BadRequestResult()
        {
            //arrange
            SeedDb();
            int favoriteid = 0;
            var userController = new UserController(_context);

            //act
            var user = await userController.RemoveFavoriteById(favoriteid);
            var result = (ObjectResult)user;
            await _context.Database.EnsureDeletedAsync();


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}