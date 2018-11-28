using SecretSanta.Domain.Interfaces.Facades;
using SecretSanta.Domain.Interfaces.Repositories;
using System;

namespace SecretSanta.Domain.Facades {
    public class SantaFacade : ISantaFacade {
        public IPersonRepository PersonRepository { get; set; }

        public SantaFacade(IPersonRepository personRepo) {
            if (personRepo is null) throw new ArgumentException();
            this.PersonRepository = personRepo;
        }

        public void DistributeHolidayCheer() {
            throw new NotImplementedException();
        }
    }          
}
