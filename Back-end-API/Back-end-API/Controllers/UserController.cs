using Azure.Core;
using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.FavoriteDTO_s;
using Back_end_API.BusinessLogic.UserDTO_s;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;

namespace Back_end_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly RecipeAppContext _context;

        VerifyInfo verify = new VerifyInfo();
        EmailCreator emailCreator = new EmailCreator();

        public UserController(RecipeAppContext context)
        {
            _context = context; 
        }

        /// <summary>
        /// Methode die een user ophaalt aan de hand van een id.
        /// </summary>
        /// <param name="id">user id van ingevulde front-end.</param>
        /// <returns>Ok wanneer user is verstuurd naar front-end.</returns>
        [HttpGet("getuser")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var myusercheck = await _context.Users.FindAsync(id);
            if (myusercheck == null) { return BadRequest("user not found"); }

            var myuser = await _context.Users
            .Where(u => u.userId == id)
                         .Select(u => new
                         {
                             u.userId,
                             u.userName,
                             u.Email,
                             u.adress,
                             u.phone,
                             u.activeSince,
                             u.isAdmin
                         })
                         .ToArrayAsync();

            return Ok(myuser);
        }

        /// <summary>
        /// Methode om alle users op te halen
        /// </summary>
        /// <returns>Ok wanneer alle users verstuurd zijn naar front-end.</returns>
        [HttpGet("getallusers")]
        public async Task<ActionResult> GetAllUsers()
        {
            var myuser = await _context.Users
                         .Select(u => new
                         {
                             u.userId,
                             u.userName,
                             u.Email,
                             u.adress,
                             u.phone,
                             u.activeSince,
                             u.isAdmin
                         })
                         .ToListAsync();

            return Ok(myuser);
        }

        /// <summary>
        /// Methode om het profiel van een user aan te passen.
        /// </summary>
        /// <param name="request">Verzameling van userId, username, adress en phone.</param>
        /// <returns>Ok wanneer user is gevonden en gegvens zijn aangepast, badrequest wanneer user niet is gevonden.</returns>
        [HttpPut("changeprofile")]
        public async Task<ActionResult> ChangeProfile(UserDTO request)
        {
            var myuser = await _context.Users.FindAsync(request.userId);
            if (myuser != null)
            {
                if (request.userName != "") { myuser.userName = request.userName; }
                if (request.adress != "") { myuser.adress = request.adress; }
                if (request.phone != "") { myuser.phone = request.phone; }

                await _context.SaveChangesAsync();
                return Ok("profile has changed");
            }
            return BadRequest("user not found");
        } 

        /// <summary>
        /// Methode om het wachtwoord van een user aan te passen.
        /// </summary>
        /// <param name="request">Verzameling van userId, currentpassword en newpassword.</param>
        /// <returns>Ok wanneer user is gevonden, wachtwoord is aangepast en email is verzonden, Badrequest wanneer user niet is gevonden.</returns>
        [HttpPut("changepassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDTO request)
        {
            var myuser = await _context.Users.FindAsync(request.userId);
            if(myuser != null && verify.VerifyPasswordHash(request.currentPassword, myuser.passwordHash, myuser.passwordSalt))
            {
                verify.CreatePasswordHash(request.newPassword, out byte[] passwordhash, out byte[] passwordsalt);

                myuser.passwordHash = passwordhash;
                myuser.passwordSalt = passwordsalt;

                await _context.SaveChangesAsync();

                //stuur email met confirmatie wachtwoord.
                emailCreator.SendEmailResetPasswordSucces(myuser.Email, myuser.userName);

                return Ok("password has changed");
            }
            return BadRequest("incorrect password");
        }

        /// <summary>
        /// Methode om te checken of username al bestaat en eigen username uit het filter halen.
        /// </summary>
        /// <param name="request">Verzameling van currentUsername en newUsername.</param>
        /// <returns>true wanneer username dubbel is en niet overeen komt met huidige username, false als er geen dubbele username is.</returns>
        [HttpPost("doubleusername")]
        public async Task<ActionResult<bool>> DoubleUsernameExcludeCurrentUserName(ChangeUsernameDTO request)
        {
            bool doubleUsername = await _context.Users.AnyAsync(u => u.userName == request.newUsername);

            if (doubleUsername && request.currentUsername.ToLower() != request.newUsername.ToLower()) { return true; }

            return false;
        }

        /// <summary>
        /// Methode om te checken of email al bestaat en eigen email uit het filter halen.
        /// </summary>
        /// <param name="request">Verzameling van currentEmail en newEmail.</param>
        /// <returns>true wanneer email dubbel is en niet overeen komt met huidige email, false als er geen dubbele email is.</returns>
        [HttpPost("doubleemail")]
        public async Task<ActionResult<bool>> DoubleEmailExcludeCurrentEmail(ChangeEmailDTO request)
        {
            bool doubleEmail = await _context.Users.AnyAsync(u => u.Email == request.newEmail);

            if (doubleEmail && request.currentEmail.ToLower() != request.newEmail.ToLower()) { return true; }

            return false;
        }

        /// <summary>
        /// Methode die het aantal gecreerde recepeten van een user ophaalt.
        /// </summary>
        /// <param name="id">user id van ingevulde front-end.</param>
        /// <returns>Ok wanneer het aantal is verstuurd naar de front-end.</returns>
        [HttpGet("recipesint")]
        public async Task<ActionResult<int>> GetUserRecipesAmountById(int id)
        {
            var createdrecipes = await _context.Recipes
                .Where(r => r.userId == id)
                .ToListAsync();

            var recipes = createdrecipes.Count();

            return recipes;
        }

        /// <summary>
        /// Methode die het aantal favorieten van een user ophaalt.
        /// </summary>
        /// <param name="id">user id van ingevulde front-end.</param>
        /// <returns>Ok wanneer het aantal is verstuurd naar de front-end.</returns>
        [HttpGet("favoritesint")]
        public async Task<ActionResult<int>> GetUserFavoritesAmountById(int id)
        {
            var favoriterecipes = await _context.Favorites
                .Where(f => f.userId == id)
                .ToListAsync();

            var favorites = favoriterecipes.Count();

            return favorites;
        }

        /// <summary>
        /// Methode om de gemaakte recepten van een user op te halen.
        /// </summary>
        /// <param name="id">userId van de ingevulde front-end.</param>
        /// <returns>Ok wanneer recepten zijn opgestuurd.</returns>
        [HttpGet("getallmyrecipes")]
        public async Task<ActionResult> GetUserRecipesById(int id)
        {
            var myrecipecheck = await _context.Users.FindAsync(id);
            if (myrecipecheck == null) { return BadRequest("user not found"); }

            var myrecipe = await _context.Recipes
                         .Where(r => r.userId == id)
                         .Select(r => new
                         {
                             r.recipeId,
                             r.Title,
                             r.Description,
                             r.Ingredients,
                             r.prepTime,
                             r.Portions,
                             r.Status,
                             createDate = r.createDate.ToString("dd-MM-yyyy"),
                             imageBase64 = Convert.ToBase64String(r.imageFile),
                             r.User.userName
                         })
                        .ToListAsync();

            return Ok(myrecipe);
        }

        /// <summary>
        /// Methode om alle favoeriten op te halen.
        /// </summary>
        /// <param name="id">FavoriteId van de ingevulde front-end.</param>
        /// <returns>Ok wannneer alle favoerieten zijn opgestuurd.</returns>
        [HttpGet("getallmyfavorites")]
        public async Task<ActionResult> GetAllFavoritesById(int id)
        {
            var myfavoritecheck = await _context.Favorites.AnyAsync(f => f.userId == id);
            if (myfavoritecheck == false) { return BadRequest("favorite not found"); }

            var myfavorite = await _context.Favorites
                         .Where(f => f.userId == id)
                         .Select(f => new
                         {
                             f.recipeId,
                             f.Recipe.Title,
                             f.Recipe.Description,
                             f.Recipe.Ingredients,
                             f.Recipe.prepTime,
                             f.Recipe.Portions,
                             imageBase64 = Convert.ToBase64String(f.Recipe.imageFile),
                             f.Recipe.User.userName
                         })
                         .ToListAsync();

            return Ok(myfavorite);
        }

        /// <summary>
        /// Methode die check of het een favoriet is of niet.
        /// </summary>
        /// <param name="request">Verzaemling van userid en recipeid.</param>
        /// <returns>true wanneer favoriet is, false als het geen favoriet is.</returns>
        [HttpPost("getfavorite")]
        public async Task<ActionResult<bool>> GetFavoriteById(FavoriteDTO request)
        {
            bool myrecipe = await _context.Favorites.AnyAsync(f => f.recipeId == request.recipeId);
            bool myuser = await _context.Favorites.AnyAsync(f => f.userId == request.userId);

            if (myrecipe && myuser) { return true; }
            else { return false; }
        }

        /// <summary>
        /// Methode om een favoriet toe te voegen/removen.
        /// </summary>
        /// <param name="request">Verzameling van userId en recipeId.</param>
        /// <returns>Ok wanneer recept is toegevoegt aan favorieten, badrequest wanneer user/recept niet is gevonden of wanneer recept al in favorieten staat.</returns>
        [HttpPost("addfavorite")]
        public async Task<ActionResult> AddToFavorites(FavoriteDTO request)
        {
            var myuser = await _context.Users.FindAsync(request.userId);
            var myrecipe = await _context.Recipes.FindAsync(request.recipeId);
            var myfavorites = await _context.Favorites.FirstOrDefaultAsync(f => f.userId == request.userId && f.recipeId == request.recipeId);

            if (myuser == null || myrecipe == null || myfavorites != null)
            {
                await RemoveFavoriteById(myfavorites.favoriteId);
                return BadRequest("favorite removed");
            }

            var newfavorite = new FavoritesModel
            {
                userId = request.userId,
                recipeId = request.recipeId,
            };

            await _context.Favorites.AddAsync(newfavorite);
            await _context.SaveChangesAsync();

            return Ok("favorite added");
        }

        /// <summary>
        /// Methode om favoriet te verwijderen.
        /// </summary>
        /// <param name="id">FavoriteId van de ingevulde front-end.</param>
        /// <returns>Ok wanneer favoriet is verwijderd.</returns>
        [NonAction]
        public async Task<ActionResult> RemoveFavoriteById(int id)
        {
            var favoritetoDelete = await _context.Favorites.FindAsync(id);
            if (favoritetoDelete == null) return BadRequest("favorite not found");

            _context.Favorites.Remove(favoritetoDelete);
            await _context.SaveChangesAsync();

            return Ok("favorite removed");
        }

        

        /// <summary>
        /// Methode om een email te sturen van user naar bedrijf.
        /// </summary>
        /// <param name="request">Verzameling van name, email, subject en body</param>
        [HttpPost("contactus")]
        public void ContactUs(ContactUsDTO request)
        {
            //stuur email met ingevulde info.
            emailCreator.RecieveEmailContactUs(request.name, request.email, request.subject, request.body);
        }
    }
}
