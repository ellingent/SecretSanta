using SecretSanta.Data;
using SecretSanta.Domain.Facades;
using SecretSanta.Domain.Factories;
using SecretSanta.Domain.Interfaces.Facades;
using SecretSanta.Domain.Mappers;

namespace Santa {
    class Program
    {
        static void Main(string[] args)
        {
            MapperConfig.Init();

            ISantaFacade facade = new SantaFacade(new PersonRepository(), new SecretSantaFactory());
            facade.DistributeHolidayCheer();
        }
    }
}
