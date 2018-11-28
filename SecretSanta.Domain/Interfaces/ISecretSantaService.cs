using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Interfaces {
    public interface ISecretSantaService
    {
        List<Person> DistributeGiftees();
        void NotifyPersons(List<Person> AllPersons);
    }
}
