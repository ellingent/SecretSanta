using AutoMapper;
using SecretSanta.Domain.Interfaces.Facades;
using SecretSanta.Domain.Interfaces.Repositories;
using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Models;
using System;
using System.Linq;

namespace SecretSanta.Domain.Facades {
    public class SantaFacade : ISantaFacade {
        public IPersonRepository PersonRepository { get; set; }
        public ISecretSantaService SantaService { get; set; }

        public SantaFacade() { }
        public SantaFacade(IPersonRepository personRepo, ISecretSantaService service) {
            if (personRepo is null || service is null) throw new ArgumentNullException();

            this.PersonRepository = personRepo;         //TODO: Update tests to inject
            this.SantaService = service;
        }

        public void DistributeHolidayCheer() {
            var personDtos = PersonRepository.GetPersons();
            if (personDtos.Any()) {
                SantaService.Participants = personDtos.Select(Mapper.Map<Person>).ToList();
                SantaService.NotifyPersons(SantaService.DistributeGiftees());
            }
        }
    }          
}
