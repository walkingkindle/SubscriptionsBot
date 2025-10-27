using Domain.Entities;

namespace Domain.Interface
{
    public interface ISubscriptionService
    {
        Task<List<Subscription>> GetSubscriptions();
    }
}
