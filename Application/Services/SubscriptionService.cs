using Domain.Entities;
using Domain.Interface;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class SubscriptionService(ApplicationDbContext dbContext) : ISubscriptionService
        {
        public Task<List<Subscription>> GetSubscriptions()
        {
            return dbContext.Subscriptions.ToListAsync();
        }

    }
}
