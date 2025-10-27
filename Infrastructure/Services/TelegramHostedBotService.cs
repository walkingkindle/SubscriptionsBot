using Domain.Interface;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace Infrastructure.Services
{
    public class TelegramHostedBotService : ITelegramHostedBotService
    {
        private readonly TelegramBotClient _botClient;

        private readonly IUpdateHandler _botUpdateHandler;

        public TelegramHostedBotService(IConfiguration configuration, IEnumerable<ICommandHandler> commandHandlers)
        {
            _botUpdateHandler = new UpdateHandler(commandHandlers);
            var token = configuration["TelegramBot:Token"] ?? throw new ArgumentException();
            _botClient = new TelegramBotClient(token);

        }
        public Task RunAsync(CancellationToken cancellationToken = default)
        {
            UpdateType[] updateTypes = new UpdateType[] { UpdateType.Message };
            _botClient.StartReceiving(_botUpdateHandler, new ReceiverOptions
            {
                AllowedUpdates = updateTypes

            },
            cancellationToken);

            Console.WriteLine("Bot is running");
            
            return Task.CompletedTask;
            

        }
    }
}
