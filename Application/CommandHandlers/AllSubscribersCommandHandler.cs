using System.Text;
using Domain.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.CommandHandlers
{
    public class AllSubscribersCommandHandler : ICommandHandler
    {

        private readonly ISubscriberService _subscriberService;

        public AllSubscribersCommandHandler(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;

        }

        public string Command => "/allsubscribers";

        public async Task HandleAsync(Message message, ITelegramBotClient client)
        {
            var subscribers = await _subscriberService.GetAllSubscribers();

            if (!subscribers.Any())
            {
                await client.SendMessage(message.Chat.Id, "No subscribers found.");
                return;
            }

            foreach (var sub in subscribers)
            {
                var text = new StringBuilder();
                text.AppendLine($"Name: {sub.FullName}");
                text.AppendLine($"Subscription: {sub.Subscription.Name}");
                text.AppendLine($"Last Payment: {sub.LastPaymentDate:yyyy-MM-dd}");
                text.AppendLine($"Balance: {sub.BalanceInRsd} RSD");

                await client.SendMessage(message.Chat.Id, text.ToString());
            }
        }
    }
}
