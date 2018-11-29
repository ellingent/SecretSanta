using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SecretSanta.Domain.Services {
    public class EmailNotificationService : INotificationService {
        private const string FROM_ADDRESS = "SecretSanta@ellingen.org";
        private const string SUBJECT = "Secret Santa";

        public void Notify(Person person) {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("Santa@ellingen.org"));
            msg.AddTo(person.Email);
            msg.SetSubject("Gift Exchange");
            msg.AddContent(MimeType.Text, string.Format("Hi {0},", person.FirstName));
            msg.AddContent(MimeType.Text, string.Format("It's almost Christmas, which means it's time for the gift exchange!You have been automatically assigned to purchase a gift for {0}.", person.Giftee.FirstName));

            var client = new SendGridClient("");
            client.SendEmailAsync(msg);
        }
    }
}
