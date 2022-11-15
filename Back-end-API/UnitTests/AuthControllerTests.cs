using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.UserDTO_s;
using Back_end_API.Controllers;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class AuthControllerTests
    {
        AuthController authController;
        VerifyInfo verifyInfo = new VerifyInfo();
        private string verifyAccountToken = null!;
        private string passwordResetToken = null!;

        public AuthControllerTests()
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

            authController = new AuthController(context);

            verifyInfo.CreatePasswordHash("test123", out byte[] passwordhash, out byte[] passwordsalt);
            passwordResetToken = verifyInfo.CreateRandomToken();
            verifyAccountToken = verifyInfo.CreateRandomToken();
            var users = new List<UserModel>
            {
                new UserModel
                {
                    userId = 1,
                    userName = "Pannekoek",
                    Email = "peter@example.com",
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
                    userId = 2,
                    userName = "Pietjeh",
                    Email = "pietjeh@example.com",
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

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        //[Fact]
        //public async Task Test_Register_OKResult()
        //{
        //    //arrange
        //    var newuser = new CreateUserDTO
        //    {
        //        UserName = "Piet",
        //        Email = "piet@example.com",
        //        Password = "piet123"
        //    };


        //    //act
        //    var user = await authController.Register(newuser);
        //    var result = (ObjectResult)user;


        //    //assert
        //    Assert.NotNull(result);
        //    Assert.Equal(200, result.StatusCode);
        //}

        [Fact]
        public async Task Test_Register_BadRequestResult()
        {
            //arrange
            var myuser = new UserDTO
            {
                userName = "Piet",
                email = "peter@example.com",
                password = "piet123"
            };


            //act
            var user = await authController.Register(myuser);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_Login_OkResult()
        {
            //arrange
            var myuser = new UserDTO
            {
                email = "pietjeh@example.com",
                password = "test123"
            };


            //act
            var user = await authController.Login(myuser);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Test_Login_BadRequestResult()
        {
            //arrange
            var myuser = new UserDTO
            {
                email = "peter@example.com",
                password = "test"
            };


            //act
            var user = await authController.Login(myuser);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        //[Fact]
        //public async Task Test_VerifyAccount_OkResult()
        //{
        //    //arrange
        //    var myuser = new ActivateUserAccountDTO
        //    {
        //        Email = "peter@example.com",
        //        activateAccountToken = activateAccountToken
        //    };


        //    //act
        //    var user = await authController.VerifyAccount(myuser);
        //    var result = (ObjectResult)user;


        //    //assert
        //    Assert.NotNull(result);
        //    Assert.Equal(200, result.StatusCode);
        //}

        [Fact]
        public async Task Test_VerifyAccount_BadRequestResult()
        {
            //arrange
            var myuser = new UserDTO
            {
                email = "peter@example.com",
                activateAccountToken = "asdawdasd"
            };


            //act
            var user = await authController.VerifyAccount(myuser);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        //[Fact]
        //public async Task Test_ForgotPassword_OkResult()
        //{
        //    //arrange
        //    string email = "pietjeh@example.com";


        //    //act
        //    var user = await authController.ForgotPassword(email);
        //    var result = (ObjectResult)user;


        //    //assert
        //    Assert.NotNull(result);
        //    Assert.Equal(200, result.StatusCode);
        //}

        [Fact]
        public async Task Test_ForgotPassword_BadRequestResult()
        {
            //arrange
            string email = "asdwadsda@example.com";


            //act
            var user = await authController.ForgotPassword(email);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        //[Fact]
        //public async Task Test_ResetPassword_OkResult()
        //{
        //    //arrange
        //    var myuser = new ResetUserPasswordDTO
        //    {
        //        Password = "test1234",
        //        passwordResetToken = passwordResetToken
        //    };


        //    //act
        //    var user = await authController.ResetPassword(myuser);
        //    var result = (ObjectResult)user;


        //    //assert
        //    Assert.NotNull(result);
        //    Assert.Equal(200, result.StatusCode);
        //}

        [Fact]
        public async Task Test_ResetPassword_BadRequestResult()
        {
            //arrange
            var myuser = new UserDTO
            {
                password = "test1234",
                passwordResetToken = "asdasdasdasd"
            };


            //act
            var user = await authController.ResetPassword(myuser);
            var result = (ObjectResult)user;


            //assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Test_DoubleUserNameChecker_True()
        {
            //arrange
            string username = "Pannekoek";


            //act
            var result = await authController.DoubleUserNameChecker(username);


            //assert
            Assert.NotNull(result);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task Test_DoubleUserNameChecker_False()
        {
            //arrange
            string username = "asdijasda";


            //act
            var result = await authController.DoubleUserNameChecker(username);


            //assert
            Assert.NotNull(result);
            Assert.False(result.Value);
        }
    }
}
