using System.Data;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Domain.Interface
{
    public interface ICommandHandler
    {
        string Command { get; }

        Task HandleAsync(Message message, ITelegramBotClient client);
    }
}
