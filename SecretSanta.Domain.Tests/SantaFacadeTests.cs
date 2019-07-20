using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Domain.Dtos;
using SecretSanta.Domain.Facades;
using SecretSanta.Domain.Interfaces.Repositories;
using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace SecretSanta.Domain.Tests {
    [TestClass]
    public class SantaFacadeTests
    {
        [TestInitialize]
        public void Init() {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<PersonDto, Person>().
                    ForMember(from => from.Email,
                              opt => opt.MapFrom(dest => new MailAddress(dest.Email)));
            });
        }

        [TestCleanup]
        public void Cleanup() {
            Mapper.Reset();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_Constructor_NullRepo() {
            //Act
            var facade = new SantaFacade(null, new SecretSantaService(null));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Test_Constructor_NullFactory() {
            //Act
            var facade = new SantaFacade(new DummyRepo(), null);
        }

        [TestMethod]
        public void Test_Constructor_PropertiesSet() {
            //Arrange
            var repo = new DummyRepo();
            var service = new SecretSantaService(null);
            
            //Act
            var facade = new SantaFacade(repo, service);

            //Assert 
            Assert.AreEqual(repo, facade.PersonRepository);
            Assert.AreEqual(service, facade.SantaService);
        }

        [TestMethod]
        public void Test_DistributeHolidayCheer_EmptyPersons () {
            //Arrange
            var repo = new Mock<IPersonRepository>();
            repo.Setup(r => r.GetPersons()).Returns(new List<PersonDto>());

            var factory = new Mock<ISecretSantaService>();

            //Act
            var facade = new SantaFacade(repo.Object, factory.Object);
            facade.DistributeHolidayCheer();

            //Assert 
            repo.Verify(r => r.GetPersons(), Times.Once);
            factory.Verify(r => r.DistributeGiftees(), Times.Never);
        }

        [TestMethod]
        public void Test_DistributeHolidayCheer_OnePerson() {
            //Arrange
            var repo = new Mock<IPersonRepository>();
            repo.Setup(r => r.GetPersons()).Returns(new[] { new PersonDto() }.ToList());

            var service = new Mock<ISecretSantaService>();

            var mappedPersons = new List<Person>();
            service.SetupSet(s => s.Participants = It.IsAny<List<Person>>()).Callback((List<Person> p) => mappedPersons.AddRange(p));

            //Act
            var facade = new SantaFacade(repo.Object, service.Object);
            facade.DistributeHolidayCheer();

            //Assert 
            repo.Verify(r => r.GetPersons(), Times.Once);
            service.VerifySet(r => r.Participants = It.IsAny<List<Person>>(), Times.Once);
            service.Verify(r => r.DistributeGiftees(), Times.Once);
            service.Verify(r => r.NotifyPersons(It.IsAny<List<Person>>()), Times.Once);
        }

        #region Private Helpers

        private class DummyRepo : IPersonRepository {
            public List<PersonDto> GetPersons() {
                throw new NotImplementedException();
            }
        }

        #endregion

    }
}
