using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Facades;
using SecretSanta.Domain.Interfaces.Repositories;
using System;
using SecretSanta.Domain.Dtos;
using System.Collections.Generic;
using Moq;
using SecretSanta.Domain.Factories;
using SecretSanta.Domain.Interfaces.Factories;
using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Models;
using System.Linq;
using AutoMapper;
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
            var facade = new SantaFacade(null, new SecretSantaFactory());
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
            var factory = new SecretSantaFactory();
            
            //Act
            var facade = new SantaFacade(repo, factory);

            //Assert 
            Assert.AreEqual(repo, facade.PersonRepository);
            Assert.AreEqual(factory, facade.SantaFactory);
        }

        [TestMethod]
        public void Test_DistributeHolidayCheer_EmptyPersons () {
            //Arrange
            var repo = new Mock<IPersonRepository>();
            repo.Setup(r => r.GetPersons()).Returns(new List<PersonDto>());

            var factory = new Mock<ISecretSantaFactory>();

            //Act
            var facade = new SantaFacade(repo.Object, factory.Object);
            facade.DistributeHolidayCheer();

            //Assert 
            repo.Verify(r => r.GetPersons(), Times.Once);
            factory.Verify(r => r.Build(It.IsAny<List<Person>>()), Times.Never);
        }

        [TestMethod]
        public void Test_DistributeHolidayCheer_OnePerson() {
            //Arrange
            var repo = new Mock<IPersonRepository>();
            repo.Setup(r => r.GetPersons()).Returns(new[] { new PersonDto() }.ToList());

            var service = new Mock<ISecretSantaService>();

            var mappedPersons = new List<Person>();
            var factory = new Mock<ISecretSantaFactory>();
            factory.Setup(f => f.Build(It.IsAny<List<Person>>())).Returns(service.Object).
                                                                  Callback((List<Person> p) => mappedPersons.AddRange(p));

            //Act
            var facade = new SantaFacade(repo.Object, factory.Object);
            facade.DistributeHolidayCheer();

            //Assert 
            repo.Verify(r => r.GetPersons(), Times.Once);
            factory.Verify(r => r.Build(mappedPersons), Times.Once);
            service.Verify(r => r.DistributeGiftees(), Times.Once);
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
