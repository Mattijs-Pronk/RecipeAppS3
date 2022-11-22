using Back_end_API.BusinessLogic;
using Microsoft.AspNetCore.Http;
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
        ImageConverter converter = new ImageConverter();

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

        [Fact]
        public void Test_UploadImage()
        {
            //arrange
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile recipeImage = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            //act
            byte[] imgbyte = converter.UploadImage(recipeImage);

            //assert
            Assert.NotNull(imgbyte);
        }
    }
}
