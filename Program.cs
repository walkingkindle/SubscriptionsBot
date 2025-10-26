using Application.CommandHandlers;
using Domain.Interface;
using Infrastructure.Services;

namespace SubscriptionsBot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var host = Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
            {
                services.AddSingleton<ICommandHandler, PingCommandHandler>();
                services.AddSingleton<ICommandHandler, AllSubscribersCommandHandler>();
                services.AddSingleton<ITelegramHostedBotService, TelegramHostedBotService>();
                var startup = new Startup();

                startup.ConfigureServices(services);
            }).Build();

            var bot = host.Services.GetRequiredService<ITelegramHostedBotService>();

            await bot.RunAsync();

            await host.RunAsync();

        }

    }
}
