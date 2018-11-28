using SecretSanta.Domain.Interfaces.Factories;
using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;

namespace SecretSanta.Domain.Factories {
    public class SecretSantaFactory : ISecretSantaFactory {
        public ISecretSantaService Build(List<Person> persons) {
            return new SecretSantaService(persons);
        }
    }
}
