using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ChatBot2000.Core;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messages;
using ChatBot2000.Infrastructure.Twitch;
using Microsoft.Extensions.Configuration;

namespace ChatBot2000.Cons
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var config = InitConfiguration();

            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: Bot starting...");

            var chatClients = ChatClients(config);

            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: Bot started successfully.");

            Console.WriteLine("Press Ctrl+C to exit...");

            var botMain = new BotMain(chatClients);
            botMain.Run();
        }

        private static IConfiguration InitConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }

        private static List<IChatClient> ChatClients(IConfiguration config)
        {
            var clients = new List<IChatClient>
            {
                new ConsoleChatClient(),
                new TwitchChatClient(config.GetSection(nameof(TwitchClientSettings)).Get<TwitchClientSettings>())
            };

            return clients;
        }
    }
}
