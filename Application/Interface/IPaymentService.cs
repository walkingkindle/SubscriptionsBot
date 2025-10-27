using Application.Dto;
using Application.Helpers;
using Domain.Entities;

namespace Application.Interface
{
    public interface IPaymentService
    {
        Task AddNewPaymentAsync(Payment payment);


        Task<Result<SubscriberPaymentDto>> GetOutstandingPaymentForSubscriber(Subscriber subscriber);

    }
}
