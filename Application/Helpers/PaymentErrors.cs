namespace Application.Helpers
{
    public class PaymentErrors
    {
        public static readonly Error NullSubscriber =
            new Error("Errors.NullSubscriber", "Can't find a subscriber with that name or Id");

    }
}
