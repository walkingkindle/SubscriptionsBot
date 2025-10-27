using Domain.Entities;

namespace Domain.Interface
{
    public interface IOutstandingPaymentsService
    {
        public decimal CalculateDueAmount(Subscriber subscriber, Subscription subscription, int subscriberCount);

    }
}
