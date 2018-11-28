using SecretSanta.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Interfaces
{
    public interface IPersonRepository
    {
        List<PersonDto> GetPersons();
    }
}
