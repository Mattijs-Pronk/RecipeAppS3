using System.Security.Cryptography;

namespace Back_end_API.BusinessLogic
{
    public class VerifyInfo
    {
        /// <summary>
        /// Methode om een hash te maken van ingevulde password.
        /// </summary>
        /// <param name="password">Ingevulde password van front-end.</param>
        /// <param name="passwordhash">Gegenereerde hash van ingevulde password.</param>
        /// <param name="passwordsalt">Gegenereerde salt van ingevulde password.</param>
        public void CreatePasswordHash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Methode om te checken of opgeslagen passwordhash overeenkomt met ingevulde password.
        /// </summary>
        /// <param name="password">Ingevulde password van front-end.</param>
        /// <param name="passwordhash">Opgeslagen hash van password.</param>
        /// <param name="passwordsalt">Opgeslagen salt van password.</param>
        /// <returns>True wanneer password en opgeslagen password hash overeen komen.</returns>
        public bool VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512(passwordsalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordhash);
            }
        }

        /// <summary>
        /// Methode om een random token aan te maken.
        /// </summary>
        /// <returns>Random string van 16 tekens.</returns>
        public string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(16));
        }
    }
}
