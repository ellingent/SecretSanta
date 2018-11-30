using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services {
    public class SendGridNotificationService : INotificationService {
        public void Notify(Person person) {
            var t = Task.Run(async () => {

                var msg = new SendGridMessage();
                msg.SetFrom(new EmailAddress("SecretSanta@ellingen.org"));
                msg.AddTo(person.Email);
                msg.SetSubject("Gift Exchange");
                msg.AddContent(MimeType.Text, string.Format("Hi {0}, {1}{1} It's almost Christmas, which means it's time for the gift exchange! You have been automatically assigned to purchase a gift for {2}.{1}{1}Warm Regards,{1}Santa", person.FirstName, Environment.NewLine, person.Giftee.FirstName));

                var client = new SendGridClient("");
                var r = await client.SendEmailAsync(msg);
            });

            t.Wait();
        }
    }
}
