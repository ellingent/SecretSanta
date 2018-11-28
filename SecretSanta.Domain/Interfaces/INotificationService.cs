using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Interfaces {
    public interface INotificationService
    {
        void Notify(Person person);
    }
}
