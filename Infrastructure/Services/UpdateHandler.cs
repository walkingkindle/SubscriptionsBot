using Domain.Interface;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Infrastructure.Services
{
    public class UpdateHandler(IEnumerable<ICommandHandler> handlers) : IUpdateHandler
    {
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message || update.Message?.Text is null)
                return;

            var message = update.Message;

            var handler = handlers.FirstOrDefault(h => h.Command == message.Text);
            if (handler != null)
            {
                await handler.HandleAsync(message, botClient);
            }
            else
            {
                await botClient.SendMessage(message.Chat.Id, "Unknown command", cancellationToken: cancellationToken);
            }
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source,
            CancellationToken cancellationToken)
        {
            Console.WriteLine($"Bot error: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}
