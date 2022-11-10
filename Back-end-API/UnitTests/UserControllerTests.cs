using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.FavoriteDTO_s;
using Back_end_API.BusinessLogic.UserDTO_s;
using Back_end_API.Controllers;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Security.Cryptography;
using Assert = Xunit.Assert;

namespace UnitTests
{
    public class UserControllerTests
    {
        UserController userController;

        public UserControllerTests()
        {
            SeedDb();
        }

        private void SeedDb()
        {
            var options = new DbContextOptionsBuilder<RecipeAppContext>()
                .UseInMemoryDatabase(databaseName: "test")
                .Options;

            var context = new RecipeAppContext(options);

            userController = new UserController(context);

            context.Database.EnsureDeleted();

            var favorite = new FavoritesModel
            {
                favoriteId = 1,
                recipeId = 1,
                userId = 1
            };

            var recipe = new RecipeModel
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
            };

            var recipe1 = new RecipeModel
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
            };

            var user = new UserModel
            {
                userId = 1,
                userName = "Peter",
                Email = "peter@example.com",
                adress = "test str.15",
                phone = "329029034",
                passwordHash = RandomNumberGenerator.GetBytes(2),
                passwordSalt = RandomNumberGenerator.GetBytes(2),
                isAdmin = false,
                activeSince = DateTime.Now,
                passwordResetToken = null,
                passwordResetTokenExpires = null,
                activateAccountToken = null,
                activateAccountTokenExpires = null
            };

            context.Users.Add(user);
            context.Recipes.Add(recipe);
            context.Recipes.Add(recipe1);
            context.Favorites.Add(favorite);
            context.SaveChanges();
        }

        [Fact]
        public void Test_GetUserById_OKResult()
        {
            //arrange
            int userid = 1;

            //act
            var user = userController.GetUserById(userid);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_GetUserById_BadRequestResult()
        {
            //arrange
            var userid = 0;

            //act
            var user = userController.GetUserById(userid);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Test_ChangeProfile_OKResult()
        {
            //arrange

            var request = new ChangeUserProfileDTO
            {
                userId = 1,
                userName = "Piet",
                adress = "van laan str.16",
                phone = "392034854"
            };

            //act
            var user = userController.ChangeProfile(request);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_ChangeProfile_BadRequestResult()
        {
            //arrange

            var request = new ChangeUserProfileDTO
            {
                userId = 0,
                userName = "Piet",
                adress = "van laan str.16",
                phone = "392034854"
            };

            //act
            var user = userController.ChangeProfile(request);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Test_DoubleUsernameChangeUsername_True()
        {
            //arrange
            var request = new ChangeUsernameDTO
            {
                currentUsername = "Piet",
                newUsername = "Peter"
            };


            //act
            var result = userController.DoubleUsernameChangeUsername(request);


            //assert
            Assert.NotNull(result);
            Assert.Equal(true, result.Result.Value);
        }

        [Fact]
        public void Test_DoubleUsernameChangeUsername_False()
        {
            //arrange
            var request = new ChangeUsernameDTO
            {
                currentUsername = "Peter",
                newUsername = "Piet"
            };


            //act
            var result = userController.DoubleUsernameChangeUsername(request);


            //assert
            Assert.NotNull(result);
            Assert.Equal(false, result.Result.Value);
        }

        [Fact]
        public void Test_GetUserRecipesAmountById_Int()
        {
            //arrange
            int userid = 1;


            //act
            var result = userController.GetUserRecipesAmountById(userid);


            //assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Result.Value);
        }

        [Fact]
        public void Test_GetUserFavoritesAmountById_Int()
        {
            //arrange
            int userid = 1;


            //act
            var result = userController.GetUserFavoritesAmountById(userid);


            //assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Result.Value);
        }

        [Fact]
        public void Test_GetUserRecipesById_OKResult()
        {
            //arrange
            int userid = 1;


            //act
            var user = userController.GetUserRecipesById(userid);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_GetUserRecipesById_BadRequestResult()
        {
            //arrange
            int userid = 0;


            //act
            var user = userController.GetUserRecipesById(userid);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Test_GetAllFavoritesById_OKResult()
        {
            //arrange
            int favoriteid = 1;


            //act
            var user = userController.GetAllFavoritesById(favoriteid);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_GetAllFavoritesById_BadRequestResult()
        {
            //arrange
            int favoriteid = 0;


            //act
            var user = userController.GetAllFavoritesById(favoriteid);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Test_GetFavoriteById_True()
        {
            //arrange
            var request = new AddFavoriteDTO
            {
                recipeId = 1,
                userId = 1,
            };


            //act
            var result = userController.GetFavoriteById(request);


            //assert
            Assert.NotNull(result);
            Assert.Equal(true, result.Result.Value);
        }

        [Fact]
        public void Test_GetFavoriteById_False()
        {
            //arrange
            var request = new AddFavoriteDTO
            {
                recipeId = 0,
                userId = 0,
            };


            //act
            var result = userController.GetFavoriteById(request);


            //assert
            Assert.NotNull(result);
            Assert.Equal(false, result.Result.Value);
        }

        [Fact]
        public void Test_AddToFavorites_OkResult()
        {
            //arrange
            var request = new AddFavoriteDTO
            {
                recipeId = 2,
                userId = 1,
            };


            //act
            var user = userController.AddToFavorites(request);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Test_AddToFavorites_BadRequestResult()
        {
            //arrange
            var request = new AddFavoriteDTO
            {
                recipeId = 1,
                userId = 1,
            };


            //act
            var user = userController.AddToFavorites(request);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Test_RemoveFavoriteById_OkResult()
        {
            //arrange
            int favoriteid = 1;


            //act
            var user = userController.RemoveFavoriteById(favoriteid);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]

        public void Test_RemoveFavoriteById_BadRequestResult()
        {
            //arrange
            int favoriteid = 0;


            //act
            var user = userController.RemoveFavoriteById(favoriteid);
            var result = user.Result as ObjectResult;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

    }
}