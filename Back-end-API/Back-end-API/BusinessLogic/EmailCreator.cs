using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace Back_end_API.BusinessLogic
{
    public class EmailCreator
    {
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

        public void SendEmailResetPasswordSucces(string email, string password, string username)
        {
            string login = "http://127.0.0.1:5173/login";
            string subject = "Cloud recipes (New Password)";

            string body = "Dear " + username + ", <br /><br /><br />Your password has succesfully been reset<br /><br />" +
                "<h2>Your new password:<h2 />" +
                "" + password + "<br /><br /><br />" +
                "Click on the link below and use your new password." +
                "<br />" + login + "<br /><br />" +
                "If you did not reset your password, you can ignore this email. " +
                "<br /> Keep this email to yourself for safety reasons." +
                "<br /><br /><br /> Kind regards," +
                "<br /><br/> Team Cloud Recipes.";

            SendEmail(email, body, subject);
        }

        private void RecieveEmail(string email, string body, string subject)
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

        public void RecieveEmailContactUs(string email, string body, string subject)
        {
            RecieveEmail(email, body, subject);
        }
    }
}
