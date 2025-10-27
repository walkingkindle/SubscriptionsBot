using System.Runtime.CompilerServices;
using System.Text;
using Application.Dto;
using Application.Interface;
using Domain.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.CommandHandlers
{
    public class PingCommandHandler(IPaymentService paymentService) : ICommandHandler
    {
        public string Command => "/ping";
        public async Task HandleAsync(Message message, ITelegramBotClient client)
        {
            await client.SendMessage(message.Chat.Id, "pong");
        }
    }
}
