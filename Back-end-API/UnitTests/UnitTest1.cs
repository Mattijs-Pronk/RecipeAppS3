using Back_end_API.Controllers;
using Back_end_API.Data;
using Back_end_API.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace UnitTests
{
    public class UnitTest1
    {
        private void Seed(RecipeAppContext context)
        {
            var favorite = new FavoritesModel
            {
                favoriteId = 1,
                recipeId = 1,
                userId = 1,
            };

            //var recipe = new RecipeModel
            //{
            //    recipeId = 1,
            //    Title = "test",
            //    imageName = "test.png",
            //    Ingredients = "test",
            //    Description = "test",
            //    prepTime = 1,
            //    Portions = 1,
            //    Rating = 1,
            //    Status = "Accepted",
            //    userId = 1,
            //};

            //var user = new UserModel
            //{
            //    userId = 1,
            //    userName = "Peter",
            //    Email = "peter@example.com",
            //    adress = "test str.15",
            //    phone = "329029034",
            //    passwordHash = RandomNumberGenerator.GetBytes(2),
            //    passwordSalt = RandomNumberGenerator.GetBytes(2),
            //    isAdmin = false,
            //    activeSince = DateTime.Now,
            //    passwordResetToken = null,
            //    passwordResetTokenExpires = null,
            //    activateAccountToken = null,
            //    activateAccountTokenExpires = null,
            //};

            context.Favorites.Add(favorite);
            context.SaveChanges();
        }

        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<RecipeAppContext>()
                .UseInMemoryDatabase(databaseName: "test")
                .Options;

            var context = new RecipeAppContext(options);

            Seed(context);

            var userController = new UserController(context);

            var result = userController.GetAllFavoritesById(1);

            result.Should().NotBeNull();
            //result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}