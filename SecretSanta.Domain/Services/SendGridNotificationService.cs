using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SecretSanta.Domain.Services {
    public class SendGridNotificationService : INotificationService {
        private const string FROM_ADDRESS = "SecretSanta@ellingen.fun.test-google-a.com";
        private const string SUBJECT = "Secret Santa";

        public void Notify(Person person) {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("SecretSanta@ellingen.fun.test-google-a.com"));
            msg.AddTo(person.Email);
            msg.SetSubject("Gift Exchange");
            msg.AddContent(MimeType.Text, string.Format("Hi {0},", person.FirstName));
            msg.AddContent(MimeType.Text, string.Format("It's almost Christmas, which means it's time for the gift exchange!You have been automatically assigned to purchase a gift for {0}.", person.Giftee.FirstName));

            var client = new SendGridClient("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");
            client.SendEmailAsync(msg);
        }
    }
}
