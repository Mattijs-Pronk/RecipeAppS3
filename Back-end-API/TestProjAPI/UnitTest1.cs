using Back_end_API.BusinessLogic;
using Back_end_API.Controllers;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Security.Cryptography;

namespace TestProjAPI
{
    public class Tests
    {
        //private static DbContextOptions<RecipeAppContext> dbContextOptions = new DbContextOptionsBuilder<RecipeAppContext>()
        //    .UseInMemoryDatabase(databaseName: "ControllerTest")
        //    .Options;

        //RecipeAppContext context;
        //UserController userController;

        //[OneTimeSetUp]
        //public void Setup()
        //{
        //    context = new RecipeAppContext(dbContextOptions);
        //    context.Database.EnsureCreated();

        //    SeedDatabase();

        //    userController = new UserController(context);
        //}

        //private void SeedDatabase()
        //{
        //    var user = new UserModel
        //    {
        //        userId = 1,
        //        userName = "Peter",
        //        Email = "peter@example.com",
        //        adress = "test str.15",
        //        phone = "329029034",
        //        passwordHash = RandomNumberGenerator.GetBytes(2),
        //        passwordSalt = RandomNumberGenerator.GetBytes(2),
        //        isAdmin = false,
        //        activeSince = DateTime.Now,
        //        passwordResetToken = null,
        //        passwordResetTokenExpires = null,
        //        activateAccountToken = null,
        //        activateAccountTokenExpires = null,
        //    };
            
        //    context.Users.AddRange(user);
        //    context.SaveChanges();
        //}

        //[Test]
        //public void Test_GetUserById()
        //{
        //    var actionResult = userController.GetUserById(1);

        //    Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
        //}
    }
}