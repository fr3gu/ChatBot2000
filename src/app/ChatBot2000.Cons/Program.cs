using System;
using System.Collections.Generic;
using System.IO;
using ChatBot2000.Core;
using ChatBot2000.Core.Data;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messaging.Interfaces;
using ChatBot2000.Infrastructure.Json;
using ChatBot2000.Infrastructure.Twitch;
using Microsoft.Extensions.Configuration;

namespace ChatBot2000.Cons
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var config = InitConfiguration();

            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: Application starting...");

            FakeData.Initialize();

            var chatClients = GetChatClients(config);

            var genericJsonFileRepository = new GenericJsonFileRepository();
            List<ICommandMessage> commandMessages = genericJsonFileRepository.List(new ActiveMessagePolicy<ICommandMessage>());

            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: Application started successfully.");

            var botMain = new BotMain(chatClients, genericJsonFileRepository, new CommandHandler(chatClients, null));

            WaitForCommands(botMain);
        }

        private static void WaitForCommands(BotMain botMain)
        {
            Console.WriteLine("==============================");
            Console.WriteLine("Available bot commands : start, stop");
            Console.WriteLine();
            Console.WriteLine("Press Ctrl+C to exit...");
            Console.WriteLine("==============================");

            var command = "start";
            while (true)
            {
                switch (command)
                {
                    case "stop":
                        Console.WriteLine("Bot stopping....");
                        botMain.Stop();
                        Console.WriteLine("Bot stopped");
                        Console.WriteLine("==============================");
                        break;

                    case "start":
                        Console.WriteLine("Bot starting....");
                        botMain.Run();
                        Console.WriteLine("Bot started");
                        Console.WriteLine("==============================");
                        break;

                    default:
                    {
                        Console.WriteLine($"'{command}' is not a valid command");
                        break;
                    }
                }
                command = Console.ReadLine();
            }
        }

        private static IConfiguration InitConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }

        private static List<IChatClient> GetChatClients(IConfiguration config)
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
