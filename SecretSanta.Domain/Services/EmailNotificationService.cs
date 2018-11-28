using SecretSanta.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Domain.Models;
using System.Net.Mail;
using System.Net;

namespace SecretSanta.Domain.Services
{
    public class EmailNotificationService : INotificationService {
        private const string FROM_ADDRESS = "SecretSanta@ellingen.org";
        private const string SUBJECT = "Secret Santa";

        public void Notify(Person person) {

            using (SmtpClient cli = new SmtpClient()) {

                cli.EnableSsl = true;
                cli.DeliveryMethod = SmtpDeliveryMethod.Network;
                cli.UseDefaultCredentials = false;
                //TODO: Email Credentials

                StringBuilder sb = new StringBuilder();

                sb.AppendLine(String.Format("Hi {0},", person.FirstName));
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine(String.Format("It's almost Christmas, which means it's time for the gift exchange! You have been automatically assigned to purchase a gift for {0}.", person.Giftee.FirstName));
                sb.AppendLine();
                sb.AppendLine("Warm Wishes,");
                sb.AppendLine("Secret Santa");

                var msg = new MailMessage("SecretSanta@ellingen.org", person.Email.Address, "Gift Exchange", sb.ToString());
                cli.Send(msg);
            }
        }
    }
}
