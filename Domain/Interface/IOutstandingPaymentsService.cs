using Domain.Entities;

namespace Domain.Interface
{
    public interface IOutstandingPaymentsService
    {
        public (decimal amountDue, int monthsCoveredByBalance) CalculateDueAmountWithBalance(
            Subscriber subscriber,
            Subscription subscription,
            int subscriberCount);

    }
}
