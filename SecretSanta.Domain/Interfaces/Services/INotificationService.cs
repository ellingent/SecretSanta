using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Interfaces.Services {
    public interface INotificationService
    {
        void Notify(Person person);
    }
}
