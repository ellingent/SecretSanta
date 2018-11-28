using System;
using System.Net.Mail;

namespace SecretSanta.Domain.Models {
    public class Person
    {
        public Guid Id { get; set; }
        public Guid FamilyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MailAddress Email { get; set; }
        public Person Giftee { get; set; }

        public Person() { }
        public Person(Guid familyId) {
            Id = Guid.NewGuid();
            FamilyId = familyId;
        }
    }
}
