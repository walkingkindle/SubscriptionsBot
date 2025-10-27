namespace Domain.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public DateTime ChargeDate { get; set; }

        public int PriceInRsd { get; set; }

        public List<Subscriber> Subscribers { get; set; } = new();
    }
}
