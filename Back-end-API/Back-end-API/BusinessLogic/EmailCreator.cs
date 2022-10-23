using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace Back_end_API.BusinessLogic
{
    public class EmailCreator
    {
        private void CreateEmail(string email, string body)
        {
            var newEmail = new MimeMessage();
            newEmail.From.Add(MailboxAddress.Parse("cloudrecipes.info@gmail.com"));
            newEmail.To.Add(MailboxAddress.Parse(email));
            newEmail.Subject = "Cloud recipe (password reset)";
            newEmail.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("cloudrecipes.info@gmail.com", "rjfgqybzkmnvkyco");
            smtp.Send(newEmail);
            smtp.Disconnect(true);
        }

        public void SendEmailResetPassword(string email, string Token, string username)
        {
            string resetpassword = "http://127.0.0.1:5173/reset";

            string body = "Dear "+username+ ", <br /><br />Need to reset your password?<br /><br />" +
                "Use your secret code!(Copy the code below)" +
                "<br />" + Token + "<br /><br />" +
                "Click on the link below and enter the secret code above." +
                "<br />" + resetpassword + "<br /><br />" +
                "If you did not forget your password, you can ignore this email." +
                "<br /><br /> Kind regards," +
                "<br /><br /> Team Cloud Recipes.";

            CreateEmail(email, body);
        }

        public void SendEmailResetPasswordSucces(string email, string password, string username)
        {
            string login = "http://127.0.0.1:5173/login";

            string body = "Dear " + username + ", <br /><br />Your password has succesfully been reset<br /><br />" +
                "Your new password:" +
                "<br />" + password + "<br /><br />" +
                "Click on the link below and use your new password." +
                "<br />" + login + "<br /><br />" +
                "If you did not reset your password, you can ignore this email. " +
                "<br /> Keep this email to yourself for safety reasons." +
                "<br /><br /> Kind regards," +
                "<br /><br/> Team Cloud Recipes.";

            CreateEmail(email, body);
        }
    }
}
