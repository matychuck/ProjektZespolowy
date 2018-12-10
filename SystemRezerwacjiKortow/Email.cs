using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SystemRezerwacjiKortow
{
    public static class Email
    {
        // wywołanie funkcji -> Email.SendEmail(parametry), nie trzeba robić jakiegoś new Email itp.
        public static void SendEmail(string subject, string body, string email, string firstName)
        {
            var fromEmail = new MailAddress(ConfigurationManager.AppSettings["EmailAddress"], ConfigurationManager.AppSettings["EmailName"]);
            var toEmail = new MailAddress(email);
            var fromEmailPassword = ConfigurationManager.AppSettings["EmailPassword"];

            string myBody = "<br/>Witaj " + firstName + ",<br/>" + body + "<br/><br/>Pozdrawiamy, <br/>Zespół najlepszych kortów w mieście";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = myBody,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }
    }
}