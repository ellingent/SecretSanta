using SecretSanta.Domain.Exceptions;
using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Domain.Services {
    public class SecretSantaService : ISecretSantaService {
        private List<Person> _AllPersons;

        public INotificationService NotificationService { get; set; }

        #region Constructor 

        public SecretSantaService(List<Person> allPersons) {
            Validate(allPersons);

            _AllPersons = allPersons;
            NotificationService = new GmailNotificationService();  //TODO: IoC
        }

        #endregion

        #region ISecretSanta Implementations

        public List<Person> DistributeGiftees() {
            var gifteesAssigned = new List<Person>();
            while (gifteesAssigned.Count < _AllPersons.Count)
                gifteesAssigned = AttemptDistribution();
            return gifteesAssigned;
        }

        private List<Person> AttemptDistribution() {
            var result = new List<Person>();

            foreach (var family in _AllPersons.GroupBy(p => p.FamilyId).OrderByDescending(f => f.Count())) {
                var assignedGiftees = result.Select(p => p.Giftee).ToList();
                var availableMembersOutsideFamily = _AllPersons.Where(p => p.FamilyId != family.Key && !assignedGiftees.Contains(p)).ToList();
                foreach (var gifter in family) {
                    if (availableMembersOutsideFamily.Any()) {
                        //Randomize list by ID
                        var giftee = availableMembersOutsideFamily.OrderBy(p => p.Id.GetHashCode()).First();
                        gifter.Giftee = giftee;
                        availableMembersOutsideFamily.Remove(giftee);
                        result.Add(gifter);
                    }
                }
            }

            return result;
        }

        public void NotifyPersons(List<Person> AllPersons) {
            foreach (var person in AllPersons)
                NotificationService.Notify(person);
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
