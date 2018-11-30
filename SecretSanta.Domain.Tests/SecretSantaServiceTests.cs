using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Exceptions;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Domain.Tests
{
    [TestClass]
    public class SecretSantaServiceTests {

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_DistributeGiftee_NullPersons() {
            //Act
            var santa = new SecretSantaService();

            //Act
            santa.DistributeGiftees();
        }

        [TestMethod, ExpectedException(typeof(DistributionException))]
        public void Test_Participants_FamilyIdNull() {
            //Act
            var santa = new SecretSantaService();


            //Act
            santa.Participants = new List<Person>(new[] { new Person(Guid.NewGuid()),
                                                          new Person(Guid.NewGuid()),
                                                          new Person()
                                                        });
                                                                
        }

        [TestMethod, ExpectedException(typeof(DistributionException))]
        public void Test_Participants_NoPersons() {
            //Arrange
            var santa = new SecretSantaService();

            //Act
            santa.Participants = new List<Person>();
        }

        [TestMethod, ExpectedException(typeof(DistributionException))]
        public void Test_Participants_OnePerson() {
            //Arrange
            var santa = new SecretSantaService();

            //Act
            santa.Participants = new List<Person>(new[] { new Person(Guid.NewGuid()) });
        }

        [TestMethod, ExpectedException(typeof(DistributionException))]
        public void Test_Participants_TwoPeople() {
            //Arrange
            var santa = new SecretSantaService();

            //Act
            santa.Participants = new List<Person>(new[] { new Person(Guid.NewGuid()), new Person(Guid.NewGuid()) });
        }

        [TestMethod, ExpectedException(typeof(DistributionException))]
        public void Test_Participants_ThreePeople_TwoInSameFamily() {
            var familyId = Guid.NewGuid();
            var p0 = new Person(familyId) { FamilyId = familyId };
            var p1 = new Person(familyId) { FamilyId = familyId };
            var p2 = new Person(Guid.NewGuid());

            var santa = new SecretSantaService();

            //Act
            santa.Participants = new List<Person>(new[] { p0, p1, p2 });
        }

        [TestMethod, ExpectedException(typeof(DistributionException))]
        public void Test_Participants_FourPeople_ThreeInSameFamily() {
            var familyId = Guid.NewGuid();
            var p0 = new Person(familyId);
            var p1 = new Person(familyId);
            var p2 = new Person(familyId);
            var p3 = new Person(Guid.NewGuid());

            var santa = new SecretSantaService();

            //Act
            santa.Participants = new List<Person>(new[] { p0, p1, p2, p3 });
        }

        [TestMethod, ExpectedException(typeof(DistributionException))]
        public void Test_Participants_FivePeople_ThreeInSameFamily() {
            var familyId0 = Guid.NewGuid();
            var familyId1 = Guid.NewGuid();

            var p0 = new Person(familyId0);
            var p1 = new Person(familyId0);
            var p2 = new Person(familyId0);
            var p3 = new Person(familyId1);
            var p5 = new Person(familyId1);

            var santa = new SecretSantaService();

            //Act
            santa.Participants = new List<Person>(new[] { p0, p1, p2, p3 });
        }

        [TestMethod, ExpectedException(typeof(DistributionException))]
        public void Test_Participants_FivePeople_FourInSameFamily() {
            var familyId0 = Guid.NewGuid();
            var familyId1 = Guid.NewGuid();

            var p0 = new Person(familyId0);
            var p1 = new Person(familyId0);
            var p2 = new Person(familyId0);
            var p3 = new Person(familyId0);
            var p4 = new Person(familyId1);

            //Act
            var santa = new SecretSantaService();

            santa.Participants = new List<Person>(new[] { p0, p1, p2, p3, p4 });
        }

        [TestMethod]
        public void Test_Participants_TwoFamilies_TwoMembersPerFamily() {
            //Arrange
            var familyId0 = Guid.NewGuid();
            var familyId1 = Guid.NewGuid();

            var p0 = new Person(familyId0);
            var p1 = new Person(familyId1);
            var p2 = new Person(familyId1);
            var p3 = new Person(familyId0);

            var santa = new SecretSantaService();

            //Act
            santa.Participants = new List<Person>(new[] { p0, p1, p2, p3 });
        }

        [TestMethod]
        public void Test_DistributeGiftees_ThreePeople_ThreeFamilies() {
            //Arrange
            var familyId0 = Guid.NewGuid();
            var familyId1 = Guid.NewGuid();
            var familyId2 = Guid.NewGuid();

            var p0 = new Person(familyId0);
            var p1 = new Person(familyId1);
            var p2 = new Person(familyId2);
            
            //Act 
            var santa = new SecretSantaService();
            santa.Participants = new List<Person>(new[] { p0, p1, p2 });

            //Assert (Due to random used in algorithm, repeat this operation a whole bunch of times)
            for (int i = 0; i < 100; i++) {
                AssertPersons(3, santa.DistributeGiftees());
            }
        }

        [TestMethod]
        public void Test_DistributeGiftees_FourPeople_TwoFamilies() {
            //Arrange
            var familyId0 = Guid.NewGuid();
            var familyId1 = Guid.NewGuid();

            var p0 = new Person(familyId0);
            var p1 = new Person(familyId1);
            var p2 = new Person(familyId1);
            var p3 = new Person(familyId0);

            //Act 
            var santa = new SecretSantaService();
            santa.Participants = new List<Person>(new[] { p0, p1, p2, p3 });

            //Assert (Due to random used in algorithm, repeat this operation a whole bunch of times)
            for (int i = 0; i < 100; i++) {
                AssertPersons(4, santa.DistributeGiftees());
            }
        }

        [TestMethod]
        public void Test_DistributeGiftees_ElevenPeople_ThreeFamilies() {
            //Arrange
            var familyId0 = Guid.NewGuid();
            var familyId1 = Guid.NewGuid();
            var familyId2 = Guid.NewGuid();


            var p0 = new Person(familyId0);
            var p1 = new Person(familyId0);
            var p2 = new Person(familyId0);
            var p3 = new Person(familyId1);
            var p4 = new Person(familyId1);
            var p5 = new Person(familyId1);
            var p6 = new Person(familyId1);
            var p7 = new Person(familyId2);
            var p8 = new Person(familyId2);
            var p9 = new Person(familyId0);
            var p10 = new Person(familyId0);


            //Act 
            var santa = new SecretSantaService();
            santa.Participants = new List<Person>(new[] { p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10 });


            //Assert (Due to random used in algorithm, repeat this operation a whole bunch of times)
            for (int i = 0; i < 100; i++) {
                AssertPersons(11, santa.DistributeGiftees());
            }
        }

        private void AssertPersons(int expectedCount, List<Person> persons) {
            Assert.AreEqual(expectedCount, persons.Count);
            Assert.IsFalse(persons.Any(p => p.Giftee is null));
            Assert.IsFalse(persons.Any(p => p.Id == p.Giftee.Id));
            Assert.IsFalse(persons.Any(p => p.FamilyId == p.Giftee.FamilyId));
            Assert.IsFalse(persons.GroupBy(p => p.Giftee.Id).Any(g => g.Count() > 1));

            var giftees = persons.Select(p => p.Giftee).ToList();
            foreach (var p in persons)
                Assert.IsTrue(giftees.Contains(p));

        }
    }
}
