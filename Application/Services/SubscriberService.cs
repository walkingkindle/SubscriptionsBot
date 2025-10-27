using Domain.Entities;
using Domain.Interface;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class SubscriberService(ApplicationDbContext dbContext) : ISubscriberService
    {

        public async Task<List<Subscriber>> GetAllSubscribers()
        {
            return await dbContext.Subscribers.Include(x => x.Subscription).ToListAsync();
        }

        public async Task<Subscriber?> GetSubscriberByName(string name)
        {
            return await dbContext.Subscribers.FirstOrDefaultAsync(x => x.FullName.StartsWith(name) || x.FullName == name);
        }
    }
}
