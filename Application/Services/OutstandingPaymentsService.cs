using Domain.Entities;
using Domain.Interface;

namespace Application.Services
{
    public class OutstandingPaymentsService : IOutstandingPaymentsService
    {
        public decimal CalculateDueAmount(Subscriber subscriber, Subscription subscription, int subscriberCount)
        {
            decimal amountPerMonth = (decimal)subscription.PriceInRsd / subscriberCount;
            DateTime now = DateTime.Now;

            int monthsDue = GetMonthsDifference(subscriber.LastPaymentDate, now);
            if (monthsDue <= 0) return 0;

            decimal totalOwed = amountPerMonth * monthsDue;

            decimal remainingAmount = totalOwed - subscriber.BalanceInRsd;

            return remainingAmount > 0 ? remainingAmount : 0;
        }

        private static int GetMonthsDifference(DateTime from, DateTime to)
        {
            return ((to.Year - from.Year) * 12) + to.Month - from.Month;
        }
    }
}
