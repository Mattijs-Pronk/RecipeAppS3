using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestAPI
{
    [TestClass]
    public class UnitTest1
    {
        private RecipeAppContext _context;
        private AuthController _authController;

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<RecipeAppContext>().UseInMemoryDatabase("Test");
            _context = new RecipeAppContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();

            _authController = new AuthController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task Test_Register()
        {
            var newUser = new CreateUserDTO
            {
                UserName = "Peter",
                Email = "peter@example.com",
                Password = "peter123"
            };

            var entityId = await _authController.Register(newUser);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.userId == 1);
            NUnit.Framework.Assert.That(newUser.UserName, Is.EqualTo(user.userName), "Names are equal");
            NUnit.Framework.Assert.That(newUser.Email, Is.EqualTo(user.Email), "Email is equal");
        }
    }
}
