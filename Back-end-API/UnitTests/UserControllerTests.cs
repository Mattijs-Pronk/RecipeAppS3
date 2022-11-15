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
        UserController userController;
        VerifyInfo verifyInfo = new VerifyInfo();

        public UserControllerTests()
        {
            SeedDb();
        }

        [Fact]
        private void SeedDb()
        {
            var options = new DbContextOptionsBuilder<RecipeAppContext>()
                .UseInMemoryDatabase(databaseName: "UserControllerTestDb")
                .Options;

            var context = new RecipeAppContext(options);

            context.Database.EnsureDeleted();

            userController = new UserController(context);

            var recipes = new List<RecipeModel>()
            {
                new RecipeModel
                {
                    recipeId = 1,
                    Title = "test",
                    Ingredients = "test",
                    Description = "test",
                    imageName = "test.png",
                    prepTime = 1,
                    Portions = 1,
                    Rating = 1,
                    Status = "Accepted",
                    userId = 1
                },
                new RecipeModel 
                {
                    recipeId = 2,
                    Title = "test",
                    Ingredients = "test",
                    Description = "test",
                    imageName = "test.png",
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
                userId = 1,
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

            context.Users.Add(user);
            context.Recipes.AddRange(recipes);
            context.Favorites.Add(favorite);
            context.SaveChanges();
        }

        [Fact]
        public async Task Test_GetUserById_OKResult()
        {
            //arrange
            int userid = 1;


            //act
            var user = await userController.GetUserById(userid);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetUserById_BadRequestResult()
        {
            //arrange
            var userid = 0;


            //act
            var user = await userController.GetUserById(userid);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_ChangeProfile_OKResult()
        {
            //arrange

            var myuser = new UserDTO
            {
                userId = 1,
                userName = "Piet",
                adress = "van laan str.16",
                phone = "392034854"
            };

            //act
            var user = await userController.ChangeProfile(myuser);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_ChangeProfile_BadRequestResult()
        {
            //arrange

            var myuser = new UserDTO
            {
                userId = 0,
                userName = "Piet",
                adress = "van laan str.16",
                phone = "392034854"
            };


            //act
            var user = await userController.ChangeProfile(myuser);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_ChangePassword_OKResult()
        {
            //arrange
            var myuser = new ChangePasswordDTO
            {
                userId = 1,
                currentPassword = "test123",
                newPassword = "test1234"
            };

            //act
            var user = await userController.ChangePassword(myuser);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_ChangePassword_BadRequestResult()
        {
            //arrange

            var myuser = new ChangePasswordDTO
            {
                userId = 0,
                currentPassword = "test123",
                newPassword = "test1234"
            };


            //act
            var user = await userController.ChangePassword(myuser);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_DoubleUsernameChangeUsername_True()
        {
            //arrange
            var myuser = new ChangeUsernameDTO
            {
                currentUsername = "Piet",
                newUsername = "Peter"
            };


            //act
            var result = await userController.DoubleUsernameExcludeCurrentUserName(myuser);


            //assert
            Assert.NotNull(result);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task Test_DoubleUsernameChangeUsername_False()
        {
            //arrange
            var myuser = new ChangeUsernameDTO
            {
                currentUsername = "Peter",
                newUsername = "Piet"
            };


            //act
            var result = await userController.DoubleUsernameExcludeCurrentUserName(myuser);


            //assert
            Assert.NotNull(result);
            Assert.False(result.Value);
        }

        [Fact]
        public async Task Test_GetUserRecipesAmountById_Int()
        {
            //arrange
            int userid = 1;


            //act
            var result = await userController.GetUserRecipesAmountById(userid);


            //assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Value);
        }

        [Fact]
        public async Task Test_GetUserFavoritesAmountById_Int()
        {
            //arrange
            int userid = 1;


            //act
            var result = await userController.GetUserFavoritesAmountById(userid);


            //assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Value);
        }

        [Fact]
        public async Task Test_GetUserRecipesById_OKResult()
        {
            //arrange
            int userid = 1;


            //act
            var user = await userController.GetUserRecipesById(userid);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetUserRecipesById_BadRequestResult()
        {
            //arrange
            int userid = 0;


            //act
            var user = await userController.GetUserRecipesById(userid);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetAllFavoritesById_OKResult()
        {
            //arrange
            int favoriteid = 1;


            //act
            var user = await userController.GetAllFavoritesById(favoriteid);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetAllFavoritesById_BadRequestResult()
        {
            //arrange
            int favoriteid = 0;


            //act
            var user = await userController.GetAllFavoritesById(favoriteid);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_GetFavoriteById_True()
        {
            //arrange
            var myfavorite = new FavoriteDTO
            {
                recipeId = 1,
                userId = 1,
            };


            //act
            var result = await userController.GetFavoriteById(myfavorite);


            //assert
            Assert.NotNull(result);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task Test_GetFavoriteById_False()
        {
            //arrange
            var myfavorite = new FavoriteDTO
            {
                recipeId = 0,
                userId = 0,
            };


            //act
            var result = await userController.GetFavoriteById(myfavorite);


            //assert
            Assert.NotNull(result);
            Assert.False(result.Value);
        }

        [Fact]
        public async Task Test_AddToFavorites_OkResult()
        {
            //arrange
            var myfavorite = new FavoriteDTO
            {
                recipeId = 2,
                userId = 1,
            };


            //act
            var user = await userController.AddToFavorites(myfavorite);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_AddToFavorites_BadRequestResult()
        {
            //arrange
            var myfavorite = new FavoriteDTO
            {
                recipeId = 1,
                userId = 1,
            };


            //act
            var user = await userController.AddToFavorites(myfavorite);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_RemoveFavoriteById_OkResult()
        {
            //arrange
            int favoriteid = 1;


            //act
            var user = await userController.RemoveFavoriteById(favoriteid);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]

        public async Task Test_RemoveFavoriteById_BadRequestResult()
        {
            //arrange
            int favoriteid = 0;


            //act
            var user = await userController.RemoveFavoriteById(favoriteid);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

    }
}