using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SecretSanta.Domain.Services {
    public class GmailNotificationService : INotificationService {
        public void Notify(Person person) {
            using (SmtpClient cli = new SmtpClient()) {

                cli.Host = "smtp.gmail.com";
                cli.Port = 587;
                cli.EnableSsl = true;
                cli.DeliveryMethod = SmtpDeliveryMethod.Network;
                cli.UseDefaultCredentials = false;
                cli.Credentials = new NetworkCredential("SecretSanta@ellingen.fun", "");

                StringBuilder sb = new StringBuilder();

                sb.AppendLine(string.Format("Hi {0},", person.FirstName));
                sb.AppendLine();
                sb.AppendLine("");
                sb.AppendLine(string.Format("It's almost Christmas, which means it's time for the gift exchange! You have been automatically assigned to purchase a gift for {0}.", person.Giftee.FirstName));
                sb.AppendLine();
                sb.AppendLine("Warm Regards,");
                sb.AppendLine("Secret Santa");

                var msg = new MailMessage("SecretSanta@ellingen.fun", person.Email.Email, "Gift Exchange", sb.ToString());
                cli.Send(msg);
            }
        }
    }
}
