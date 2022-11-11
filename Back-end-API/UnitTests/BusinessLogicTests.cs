using Back_end_API.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class BusinessLogicTests
    {
        VerifyInfo verifyInfo = new VerifyInfo();

        [Fact]
        public void Test_CreatePasswordHash_VerifyPasswordHash()
        {
            //arrange
            string password = "test123";

            //act
            verifyInfo.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordsalt);

            var result = verifyInfo.VerifyPasswordHash(password, passwordHash, passwordsalt);

            //assert
            Assert.NotNull(passwordHash);
            Assert.NotNull(passwordsalt);
            Assert.True(result);
        }

        [Fact]
        public void Test_CreateRandomToken()
        {
            //arrange


            //act
            string token = verifyInfo.CreateRandomToken();

            //assert
            Assert.NotNull(token);
        }
    }
}
