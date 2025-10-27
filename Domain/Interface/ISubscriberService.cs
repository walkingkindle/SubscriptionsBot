using Domain.Entities;

namespace Domain.Interface
{
    public interface ISubscriberService
    {
        Task<List<Subscriber>> GetAllSubscribers();

        Task<Subscriber?> GetSubscriberByName(string name);

    }
}
