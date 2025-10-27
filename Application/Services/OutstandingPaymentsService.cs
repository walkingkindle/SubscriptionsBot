using Domain.Entities;
using Domain.Interface;

namespace Application.Services
{
    public class OutstandingPaymentsService : IOutstandingPaymentsService
    {
        public (decimal amountDue, int monthsCoveredByBalance) CalculateDueAmountWithBalance(
        Subscriber subscriber,
        Subscription subscription,
        int subscriberCount)
        {
            decimal amountPerMonth = (decimal)subscription.PriceInRsd / subscriberCount;
            DateTime now = DateTime.Now;

            int monthsDue = GetMonthsDifference(subscriber.LastPaymentDate, now);
            if (monthsDue <= 0)
                return (0, 0);

            decimal totalOwed = amountPerMonth * monthsDue;

            int monthsCoveredByBalance = (int)(subscriber.BalanceInRsd / amountPerMonth);

            decimal remainingAmount = totalOwed - subscriber.BalanceInRsd;

            if (remainingAmount < 0)
                remainingAmount = 0;

            return (remainingAmount, monthsCoveredByBalance);
        }
        private static int GetMonthsDifference(DateTime from, DateTime to)
        {
            return ((to.Year - from.Year) * 12) + to.Month - from.Month;
        }
    }
}
