using SecretSanta.Domain.Dtos;
using SecretSanta.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SecretSanta.Data {
    public class PersonRepository : IPersonRepository {
        public List<PersonDto> GetPersons() {
            var secretSanta = XDocument.Load(@"C:\tmp\SecretSanta.dev.xml");
            return secretSanta.Root.Element("Persons").
                                    Elements("Person").
                                    Select(p => new PersonDto { Id = Guid.NewGuid(),
                                                                FamilyId = (Guid)p.Element("FamilyID"),
                                                                FirstName = (string)p.Element("FirstName"),
                                                                LastName = (string)p.Element("LastName"),
                                                                Email = (string)p.Element("Email")}).
                                    ToList();
        }
    }
}
