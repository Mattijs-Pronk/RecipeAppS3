﻿using Back_end_API.BusinessLogic;
using Back_end_API.BusinessLogic.UserDTO_s;
using Back_end_API.Data;
using Back_end_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_end_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        VerifyInfo verify = new VerifyInfo();
        EmailCreator emailCreator = new EmailCreator();

        public readonly RecipeAppContext _context;

        public AuthController(RecipeAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Methode om een account aan te maken.
        /// </summary>
        /// <param name="request">verzameling van username, email en wachtwoord.</param>
        /// <returns>Ok wanneer geen dubbele user is gevonden, wachtwoord is gehashed en account is aangemaakt, Badrequest wanneer dubbele user is gevonden.</returns>
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserDTO request)
        {
            bool doubleEmail = await _context.Users.AnyAsync(u => u.Email == request.email);
            if (doubleEmail)
            {
                return BadRequest("username or email already taken");
            }

            verify.CreatePasswordHash(request.password, out byte[] passwordhash, out byte[] passwordsalt);

            var newUser = new UserModel
            {
                userName = request.userName,
                Email = request.email,
                passwordHash = passwordhash,
                passwordSalt = passwordsalt,
                isAdmin = false,
                //token toevoegen.
                activateAccountToken = verify.CreateRandomToken(),
                //user moet binnen 5 dagen zijn account activeren.
                activateAccountTokenExpires = DateTime.Now.AddDays(5)
            };

            //stuur email met link van het verifieren/activeren van account.
            emailCreator.SendEmailVerifyAccount(request.email, newUser.activateAccountToken, request.userName);

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok("user created");
        }

        /// <summary>
        /// Methode om in te loggen.
        /// </summary>
        /// <param name="request">verzameling van email en wachtwoord van ingevulde front-end.</param>
        /// <returns>Ok wanneer user is gevonden, email en wachtwoord overeen komen, badrequest wanneer user niet is gevonden of email en wachtwoord niet overeenkomen.</returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserDTO request)
        {
            var Myuser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.email);

            if (Myuser != null)
            {
                if (verify.VerifyPasswordHash(request.password, Myuser.passwordHash, Myuser.passwordSalt) && Myuser.activateAccountTokenExpires == null)
                {
                    return Ok(Myuser.userId);
                }
            }
            return BadRequest("user not found");
        }

        /// <summary>
        /// Methode om een aangemaakt account te activeren.
        /// </summary>
        /// <param name="request">verzameling van email en activatieToken van ingevulde front-end.</param>
        /// <returns>Ok wanneer user is gevonden en account is geactiveerd, Badrequest wanneer user niet is gevonden.</returns>
        [HttpPost("verify")]
        public async Task<ActionResult> VerifyAccount(UserDTO request)
        {
            var Myuser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.email);

            if (Myuser != null)
            {
                if (Myuser.activateAccountToken == request.activateAccountToken)
                {
                    //token weghalen.
                    Myuser.activateAccountToken = null;
                    Myuser.activateAccountTokenExpires = null;
                    Myuser.activeSince = DateTime.Today;

                    await _context.SaveChangesAsync();
                    return Ok("user verified");
                }
            }
            return BadRequest("user not found");
        }

        /// <summary>
        /// Methode om een vergeten wachtwoord aan te vragen.
        /// </summary>
        /// <param name="email">email van ingevulde front-end.</param>
        /// <returns>Ok wanneer user is gevonden en email is verzonden, Badrequest wanneer user niet is gevonden.</returns>
        [HttpPost("forgotpassword")]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            var Myuser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (Myuser != null)
            {
                Myuser.passwordResetToken = verify.CreateRandomToken();

                //user moet binnen 15 minuten wachtwoord resetten.
                Myuser.passwordResetTokenExpires = DateTime.Now.AddMinutes(15);
                await _context.SaveChangesAsync();

                //stuur email met link van het resetten van password.
                emailCreator.SendEmailResetPassword(email, Myuser.passwordResetToken, Myuser.userName);

                return Ok("forgot password");
            }
            return BadRequest("user not found");
        }

        /// <summary>
        /// Methode om het vergeten wachtwoord aan te passen.
        /// </summary>
        /// <param name="request">verzameling van het verkregen resetToken en het nieuwe wachtwoord.</param>
        /// <returns>Ok wanneer user is gevonden, email is verstuurd en wachtwoord is aangepast, Badrequest wanneer user niet is gevonden.</returns>
        [HttpPost("resetpassword")]
        public async Task<ActionResult> ResetPassword(UserDTO request)
        {
            var Myuser = await _context.Users
                .FirstOrDefaultAsync(u => u.passwordResetToken == request.passwordResetToken);

            if(Myuser != null)
            {
                if(Myuser.passwordResetTokenExpires > DateTime.Now)
                {
                    verify.CreatePasswordHash(request.password, out byte[] passwordhash, out byte[] passwordsalt);
                    Myuser.passwordHash = passwordhash;
                    Myuser.passwordSalt = passwordsalt;
                    //token weghalen.
                    Myuser.passwordResetToken = null;
                    Myuser.passwordResetTokenExpires = null;

                    //stuur email met confirmatie wachtwoord.
                    emailCreator.SendEmailResetPasswordSucces(Myuser.Email, Myuser.userName);

                    await _context.SaveChangesAsync();
                    return Ok("password is reset");
                }
            }
            return BadRequest("user not found");
        }


        /// <summary>
        /// Methode om te checken of ingevulde username al bestaat in de database.
        /// </summary>
        /// <param name="username">gebruikersnaam van ingevulde front-end.</param>
        /// <returns>true wanneer username al in gebruik is, false wanneer username niet ingebruik is.</returns>
        [HttpPost("checkusername")]
        public async Task<ActionResult<bool>> DoubleUserNameChecker(string username)
        {
            bool doubleUsername = await _context.Users.AnyAsync(u => u.userName == username);

            if (doubleUsername) { return true; }

            return false;
        }
    }
}
