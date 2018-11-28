using SecretSanta.Data;
using SecretSanta.Domain.Facades;
using SecretSanta.Domain.Interfaces.Facades;

namespace Santa {
    class Program
    {
        static void Main(string[] args)
        {
            ISantaFacade facade = new SantaFacade(new PersonRepository());
            facade.DistributeHolidayCheer();
        }
    }
}
