using AutoMapper;
using SecretSanta.Domain.Interfaces.Facades;
using SecretSanta.Domain.Interfaces.Factories;
using SecretSanta.Domain.Interfaces.Repositories;
using SecretSanta.Domain.Models;
using System;
using System.Linq;

namespace SecretSanta.Domain.Facades {
    public class SantaFacade : ISantaFacade {
        public IPersonRepository PersonRepository { get; set; }
        public ISecretSantaFactory SantaFactory { get; set; }

        public SantaFacade(IPersonRepository personRepo, ISecretSantaFactory factory) {
            if (personRepo is null || factory is null) throw new ArgumentNullException();

            this.PersonRepository = personRepo;         //TODO: IoC
            this.SantaFactory = factory;                //TODO: IoC
        }

        public void DistributeHolidayCheer() {
            var personDtos = PersonRepository.GetPersons();
            if (personDtos.Any()) {
                var persons = personDtos.Select(Mapper.Map<Person>).ToList();
                var service = SantaFactory.Build(persons);
                service.NotifyPersons(service.DistributeGiftees());
            }
        }
    }          
}
