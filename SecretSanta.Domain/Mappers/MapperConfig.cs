using AutoMapper;
using SecretSanta.Domain.Dtos;
using SecretSanta.Domain.Models;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Mappers {
    public class MapperConfig {
        public static void Init() {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<PersonDto, Person>().
                    ForMember(from => from.Email,
                              opt => opt.MapFrom(dest => new EmailAddress(dest.Email, null)));
            });
        }
    }
}
