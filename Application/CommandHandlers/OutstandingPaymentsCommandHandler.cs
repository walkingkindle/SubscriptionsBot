using System.Security.Cryptography.X509Certificates;
using System.Text;
using Application.Dto;
using Application.Interface;
using Domain.Interface;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.CommandHandlers
{
    public class OutstandingPaymentsCommandHandler(ApplicationDbContext dbContext, IPaymentService paymentService) : ICommandHandler
    {
        public string Command => "/subscriber";
        
         public async Task HandleAsync(Message message, ITelegramBotClient botClient)
         {
            if (message.From.Id is 0)
                await botClient.SendMessage(message.Chat.Id, "🚫 Access denied.");

            var subscriber = await dbContext.Subscribers.FirstOrDefaultAsync(x =>
                x.TelegramUserId == message.From.Id ||
                (x.FirstName == message.From.FirstName && x.LastName == message.From.LastName));

            if (subscriber == null)
            {
                await botClient.SendMessage(message.Chat.Id, "🚫 Access denied.");
            }

            var result = await paymentService.GetOutstandingPaymentForSubscriber(subscriber);

            if (result.IsFailure)
            {
                await botClient.SendMessage(message.Chat.Id, result.Error.Description);
            }

            var response = ComponseMessage(result.Value);

            await botClient.SendMessage(message.Chat.Id, response);

        }

        private string ComponseMessage(SubscriberPaymentDto dto)
        {
            var sb = new StringBuilder();

            sb.AppendLine("📄 *Subscriber Payment Details*");
            sb.AppendLine();
            sb.AppendLine($"👤 *Name:* {dto.SubscriberName}");
            sb.AppendLine($"💳 *Subscription:* {dto.Subscription}");
            sb.AppendLine($"💰 *Current Balance:* {dto.CurrentBalance} RSD");
            sb.AppendLine($"📅 *Last Payment:* {dto.LastPaymentDate:yyyy-MM-dd}");
            sb.AppendLine($"⚠️ *Amount Due:* {dto.AmountDue} RSD");

            return sb.ToString();
        }
    }
}
