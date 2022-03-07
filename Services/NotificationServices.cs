using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Services
{
    public class NotificationServices : INotificationServices
    {
        public async Task SendNewUserCreation(string userName, string email)
        {
            try
            {
                var from = new EmailAddress(@"registration@alkemychallenge.com");
                var to = new EmailAddress(email);
                var subject = "Registro en Alkemy Challenge .";
                var msgText = $@"{userName} te registraste correctamente. Ya puedes ingresar con tu contraseña.";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, msgText, msgText);

                var client = new SendGridClient(AppConfiguration.MailServicesKey);

                var response = await client.SendEmailAsync(msg);

                if (!response.IsSuccessStatusCode)
                    Console.WriteLine($"NotificationServices.SendNewUserCreation: The message to {userName} can't be send.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"NoficationServices.SendNewUserCreation: {e.Message}");
                throw;
            }
        }
    }
}
