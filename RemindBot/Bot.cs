﻿using RemindBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace RemindBot
{
    internal class Bot
    {
        private static string Key = Secrets.Const.TelegramBotToken;

        private static TelegramBotClient client;
        private static List<Command> commandsList;
        public static IReadOnlyList<Command> Commands { get => commandsList.AsReadOnly(); }
        public static async Task<TelegramBotClient> Get()
        {
            if (client != null)
            {
                return client;
            }

            commandsList = new List<Command>
            {
                new Start()
            };

            client = new TelegramBotClient(Key);
            var cts = new CancellationTokenSource();
            
            await client.ReceiveAsync(new DefaultUpdateHandler(updateHandler: HandleUpdateAsync, pollingErrorHandler: UpdateHandler.HandleErrorAsync), cancellationToken: cts.Token);

            return client;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient arg1, Update update, CancellationToken cancellationToken)
        {
            await UpdateHandler.Handle(arg1, update, cancellationToken, Commands, client);
        }
    }
}
