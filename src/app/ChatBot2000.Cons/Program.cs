using System;
using System.Collections.Generic;
using System.IO;
using ChatBot2000.Core;
using ChatBot2000.Core.Data;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messaging;
using ChatBot2000.Infrastructure.Ef;
using ChatBot2000.Infrastructure.Twitch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ChatBot2000.Cons
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var config = InitConfiguration();

            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: Application starting...");

            var options = new DbContextOptionsBuilder<AppDataContext>()
                .UseInMemoryDatabase(databaseName: "fake-data-db")
                .Options;

            var efGenericRepo = new EfGenericRepo(new AppDataContext(options));

            new FakeData(efGenericRepo).Initialize();

            var chatClients = GetChatClients(config);

            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: Application started successfully.");

            var commandMessages = efGenericRepo.List(StatusPolicy<SimpleResponseMessage>.ActiveOnly());
            var commandHandler = new CommandHandler(chatClients, commandMessages);
            var botMain = new BotMain(chatClients, efGenericRepo, commandHandler);

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
