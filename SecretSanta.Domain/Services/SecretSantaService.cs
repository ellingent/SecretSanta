using SecretSanta.Domain.Exceptions;
using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Domain.Services {
    public class SecretSantaService : ISecretSantaService {
        private List<Person> _AllPersons;

        #region Constructor 

        public SecretSantaService(List<Person> allPersons) {
            Validate(allPersons);

            _AllPersons = allPersons;
        }

        #endregion

        #region ISecretSanta Implementations

        public List<Person> DistributeGiftees() {
            var result = new List<Person>();
            var rando = new Random(Guid.NewGuid().GetHashCode());   //Seed with random integer

            var gifterIndex = 0;
            while (_AllPersons.Count > result.Count) {
                var gifter = _AllPersons[gifterIndex];
                var potentialGiftee = _AllPersons[rando.Next(_AllPersons.Count)];

                if (gifter.FamilyId != potentialGiftee.FamilyId && gifter.Id != potentialGiftee.Id) {
                    gifter.Giftee = potentialGiftee;
                    result.Add(gifter);

                    gifterIndex++;
                }
            }

            return result;
        }

        public void NotifyPersons(List<Person> AllPersons) {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Helpers

        private void Validate(List<Person> allPersons) {
            EachSantaCanBeSecret(allPersons);
            AllPersonsHaveAFamily(allPersons);
            EnoughCombinationsExist(allPersons);
        }

        private void EachSantaCanBeSecret(List<Person> allPersons) {
            if (allPersons is null) throw
                    new ArgumentNullException();
            if (allPersons.Count <= 2) throw
                    new DistributionException();
        }

        private void AllPersonsHaveAFamily(List<Person> allPersons) {
            if (allPersons.Any(p => p.FamilyId == Guid.Empty))
                throw new DistributionException();
        }

        private void EnoughCombinationsExist(List<Person> allPersons) {
            var families = allPersons.GroupBy(p => p.FamilyId).ToList();

            var largestFamilySize = families.Max(g => g.Count());
            var smallestFamilySize = families.Min(g => g.Count());

            var largestFamilyId = families.OrderByDescending(f => f.Count()).First().Key;
            var personsOutsideLargestFamily = allPersons.Where(f => f.FamilyId != largestFamilyId).Count();

            if (largestFamilySize != smallestFamilySize && !(largestFamilySize < personsOutsideLargestFamily))
                throw new DistributionException();
        }

        #endregion
    }
}
