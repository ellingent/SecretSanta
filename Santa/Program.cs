using AutoMapper;
using SecretSanta.Data;
using SecretSanta.Domain.Dtos;
using SecretSanta.Domain.Facades;
using SecretSanta.Domain.Factories;
using SecretSanta.Domain.Interfaces.Facades;
using SecretSanta.Domain.Models;
using System.Net.Mail;

namespace Santa {
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureAutomapper();

            ISantaFacade facade = new SantaFacade(new PersonRepository(), new SecretSantaFactory());
            facade.DistributeHolidayCheer();
        }

        private static void ConfigureAutomapper() {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<PersonDto, Person>().
                    ForMember(from => from.Email, 
                              opt => opt.MapFrom(dest => new MailAddress(dest.Email)));
            });
        }
    }
}
