using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Subscriptions.db"); 
        }
    }
}
