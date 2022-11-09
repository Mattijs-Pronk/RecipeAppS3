using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace Back_end_API.BusinessLogic
{
    public class EmailCreator
    {
        /// <summary>
        /// Methode om een email te versturen via smtp.
        /// </summary>
        /// <param name="email">ingevulde user email.</param>
        /// <param name="body">ingevulde body van bedrijf.</param>
        /// <param name="subject">ingevulde subject van bedrijf.</param>
        private void SendEmail(string email, string body, string subject)
        {
            var newEmail = new MimeMessage();
            newEmail.From.Add(MailboxAddress.Parse("cloudrecipes.info@gmail.com"));
            newEmail.To.Add(MailboxAddress.Parse(email));
            newEmail.Subject = subject;
            newEmail.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("cloudrecipes.info@gmail.com", "rjfgqybzkmnvkyco");
            smtp.Send(newEmail);
            smtp.Disconnect(true);
        }

        /// <summary>
        /// Methode om parameters mee te geven aan "SendEmail".
        /// </summary>
        /// <param name="email">ingevulde user email.</param>
        /// <param name="Token">gegenereerde one use time bound token.</param>
        /// <param name="username">ingevulde username.</param>
        public void SendEmailVerifyAccount(string email, string Token, string username)
        {
            string verifyaccount = "http://127.0.0.1:5173/verify";
            string subject = "Cloud recipes (Verify Account)";

            string body = "Dear " + username + ", <br /><br /><br />Need to verify your account?<br /><br />" +
                "<h2>Use your secret code!(Copy the code below)<h2 />" +
                "" + Token + "<br /><br /><br />" +
                "Click on the link below and enter the secret code above." +
                "<br />" + verifyaccount + "<br /><br />" +
                "If you did not create an account, you can ignore this email." +
                "<br /><br /><br /> Kind regards," +
                "<br /><br /> Team Cloud Recipes.";

            SendEmail(email, body, subject);
        }

        /// <summary>
        /// Methode om parameters mee te geven aan "SendEmail".
        /// </summary>
        /// <param name="email">ingevulde user email.</param>
        /// <param name="Token">gegenereerde one use time bound token.</param>
        /// <param name="username">ingevulde username.</param>
        public void SendEmailResetPassword(string email, string Token, string username)
        {
            string resetpassword = "http://127.0.0.1:5173/reset";
            string subject = "Cloud recipes (Reset Password)";

            string body = "Dear " + username + ", <br /><br /><br />Need to reset your password?<br /><br />" +
                "<h2>Use your secret code!(Copy the code below)<h2 />" +
                "" + Token + "<br /><br /><br />" +
                "Click on the link below and enter the secret code above." +
                "<br />" + resetpassword + "<br /><br />" +
                "If you did not forget your password, you can ignore this email." +
                "<br /><br /><br /> Kind regards," +
                "<br /><br /> Team Cloud Recipes.";

            SendEmail(email, body, subject);
        }

        /// <summary>
        /// Methode om user op de hoogte te brengen dat zijn wachtwoord is verandert.
        /// </summary>
        /// <param name="email">ingevulde user email.</param>
        /// <param name="username">ingevulde username.</param>
        public void SendEmailResetPasswordSucces(string email, string username)
        {
            string login = "http://127.0.0.1:5173/login";
            string subject = "Cloud recipes (New Password)";

            string body = "Dear " + username + ", <br /><br /><br />Your password has succesfully been changed <br /><br />" +
                "<h2>Your password has been changed at: "+DateTime.Now.ToString()+"<h2 /> <br/> <br/>" +
                "Click on the link below and use your new password." +
                "<br />" + login + "<br /><br />" +
                "If you did not reset your password, you can ignore this email. " +
                "<br /> Keep this email to yourself for safety reasons." +
                "<br /><br /><br /> Kind regards," +
                "<br /><br/> Team Cloud Recipes.";

            SendEmail(email, body, subject);
        }

        /// <summary>
        /// Methode om een email te ontvangen van een user.
        /// </summary>
        /// <param name="name">Ingevulde name van guest.</param>
        /// <param name="email">Ingevulde email van guest.</param>
        /// <param name="body">Ingevulde body van guest.</param>
        /// <param name="subject">Ingevulde subject van guest.</param>
        private void RecieveEmail(string name, string email, string body, string subject)
        {
            var newEmail = new MimeMessage();
            newEmail.From.Add(MailboxAddress.Parse(email));
            newEmail.To.Add(MailboxAddress.Parse("cloudrecipes.info@gmail.com"));
            newEmail.Subject = subject;
            newEmail.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("cloudrecipes.info@gmail.com", "rjfgqybzkmnvkyco");
            smtp.Send(newEmail);
            smtp.Disconnect(true);
        }

        /// <summary>
        /// Methode om parameters mee te geven aan "ReceiveEmail".
        /// </summary>
        /// <param name="name">Ingevulde name van guest.</param>
        /// <param name="email">Ingevulde email van guest.</param>
        /// <param name="subject">Ingevulde body van guest.</param>
        /// <param name="body">Ingevulde subject van guest.</param>
        public void RecieveEmailContactUs(string name, string email, string subject, string body)
        {
            string bodysetup = "Sender name: " + name + " <br/> Sender email: " + email + " <br/> Sender subject: " + subject + " <br/><br/><br/> " + body + "";

            RecieveEmail(name, email, bodysetup, subject);
        }
    }
}
