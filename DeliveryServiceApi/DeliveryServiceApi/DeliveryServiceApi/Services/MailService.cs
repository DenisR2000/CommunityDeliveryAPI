using System;
using System.Net;
using System.Net.Mail;

namespace DeliveryServiceApi.Services
{
    public static class MailService
    {
        public static void SendLoginAndPassword(string Login, string Password, string StudentEmail)
        {
            MailMessage mail_message = new MailMessage();
            mail_message.IsBodyHtml = true;
            mail_message.Subject = "BaliExpress";
            mail_message.From = new MailAddress("BaliExpress@gmail.com", "Ваш новый логин и пароль");
            mail_message.To.Add(StudentEmail);
            //mail_message.Attachments.Add(new Attachment(@"C:\Users\Denis\Pictures\porshcke.jpg"));
            mail_message.Body = $"<h1>Ваш логин и пароль для входа в личный кабинет:</h1>\n<h2>Login - {Login}\nPassword - {Password}</h2>";
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {
                client.Credentials = new NetworkCredential("denisdrokson@gmail.com", "tolik050103SAND");
                client.Port = 587; //порт 587 (сервисы гугла) либо 465 (яндекс)
                client.EnableSsl = true;
                client.Send(mail_message);
            }
            //count++;
        }

        public static bool SendEmailTwoFactorCode(string userEmail, string code, string subject, string messageForUser)
        {

            var fromAddress = new MailAddress("messagess40@gmail.com", "BaliExpress");
            var toAddress = new MailAddress(userEmail, "User");
            //const string fromPassword = "MessageForYou";
            const string fromPassword = "gpxwyuybsafwdgdm";

            //const string subject = "Confirm your email";//"Two Factor Authorization Code";

            string body = $"<div style='text-align:center;'>" +
                          $"<div>" +
                          $"<h1>{messageForUser}</h1>" +
                          $"<h2 style='text-align: justify;'>" +
                          $"<a style='background: #AEB9F1; color: white; padding: 10px; border-radius: 16px; text-decoration: none;' href={code}>Підтвердити запит на зміну паролю</a>" +
                          $"</h2>" +
                          $"</div>" +
                          $"</div>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,//587
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            })
                try
                {

                    smtp.SendMailAsync(message).Wait();
                    //smtp.Send(message);     
                    //client.Send(mailMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            return false;
        }
    }
}

