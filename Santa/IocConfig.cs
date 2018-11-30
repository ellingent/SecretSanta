using Autofac;
using SecretSanta.Data;
using SecretSanta.Domain.Facades;
using SecretSanta.Domain.Factories;
using SecretSanta.Domain.Interfaces.Facades;
using SecretSanta.Domain.Interfaces.Factories;
using SecretSanta.Domain.Interfaces.Repositories;
using SecretSanta.Domain.Interfaces.Services;
using SecretSanta.Domain.Services;

namespace Santa {
    public static class IocConfig {
        public static IContainer Build() {
            var builder = new ContainerBuilder();

            builder.RegisterType<SantaFacade>().As<ISantaFacade>();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>();
            builder.RegisterType<GmailNotificationService>().As<INotificationService>();
            builder.RegisterType<SecretSantaFactory>().As<ISecretSantaFactory>(); 

            return builder.Build();
        }
    }
}
