using Application.Dto;
using Application.Helpers;
using Application.Interface;
using Domain.Entities;
using Domain.Interface;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class PaymentService(ApplicationDbContext dbContext, IOutstandingPaymentsService outstandingPaymentsService, ISubscriberService subscriberService) : IPaymentService
    {
        public async Task AddNewPaymentAsync(Payment payment)
        {
            dbContext.Payments.Add(payment);

            await dbContext.SaveChangesAsync();
        }

        public async Task<Result<SubscriberPaymentDto>> GetOutstandingPaymentForSubscriber(Subscriber subscriber)
        {
            var subscription = await dbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id == subscriber.SubscriptionId);

            if (subscription == null)
                return Result.Failure<SubscriberPaymentDto>(PaymentErrors.NullSubscriber);

            var subscriberCount = dbContext.Subscribers.Count(x => x.SubscriptionId == subscriber.SubscriptionId);
            if (subscriberCount == 0)
                return Result.Failure<SubscriberPaymentDto>(PaymentErrors.NullSubscriber);

            var amountDue = outstandingPaymentsService.CalculateDueAmount(subscriber, subscription, subscriberCount);

            SubscriberPaymentDto dto =  new SubscriberPaymentDto()
            {
                AmountDue = (int)amountDue,
                CurrentBalance = subscriber.BalanceInRsd,
                LastPaymentDate = subscriber.LastPaymentDate,
                SubscriberName = subscriber.FullName,
                Subscription = subscription.Name
            };

            return Result.Success(dto);


        }
    }
}
