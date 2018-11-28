using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Interfaces.Factories {
    public interface ISecretSantaFactory
    {
        ISecretSantaService Build(List<Person> persons);
    }
}
