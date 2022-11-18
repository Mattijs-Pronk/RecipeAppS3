namespace Back_end_API.BusinessLogic
{
    public class ImageConverter
    {
        public byte[] UploadImage(IFormFile request)
        {
            using (var ms = new MemoryStream())
            {
                request.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return fileBytes;
            }
        }
    }
}
