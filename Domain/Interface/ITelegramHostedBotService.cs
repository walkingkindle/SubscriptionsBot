namespace Domain.Interface
{
    public interface ITelegramHostedBotService
    {
        public Task RunAsync(CancellationToken token = default);
    }
}
