namespace Domain.Entities
{
    public class Subscriber
    {
        public int Id { get; set; }

        public int SubscriptionId { get; set; }

        public required Subscription Subscription { get; set; }

        public required string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime LastPaymentDate { get; set; }

        public int BalanceInRsd { get; set; }

        public long? TelegramUserId { get; set; }
    }
}
