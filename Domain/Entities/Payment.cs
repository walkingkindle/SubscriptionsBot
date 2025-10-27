namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int PaymentAmount { get; set; }

        public int SubscriberId { get; set; }

        public required Subscriber Subscriber { get; set; }

        public int SubscriptionId { get; set; }

        public required Subscription Subscription { get; set; }
    }
}
