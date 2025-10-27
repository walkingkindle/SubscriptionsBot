namespace Application.Dto
{
    public class SubscriberPaymentDto
    {
        public required string SubscriberName{get;set;}

        public required int CurrentBalance { get; set; }

        public required DateTime LastPaymentDate { get; set; }
        public required string Subscription { get; set; }

        public required int MonthlyPayment { get; set; }

        public required int AmountDue { get; set; }

        public required string NextPaymentMonth { get; set; }
    }
}
