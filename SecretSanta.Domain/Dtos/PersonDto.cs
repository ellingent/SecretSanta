using System;

namespace SecretSanta.Domain.Dtos {
    public class PersonDto
    {
        public Guid Id { get; set; }
        public Guid FamilyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
