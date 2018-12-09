using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using ChatBot2000.Core;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messages;
using ChatBot2000.Infrastructure.Twitch;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.Json;

namespace ChatBot2000.Cons
{
    internal class Program
    {
        private static IConfiguration _config;

        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _config = builder.Build();

            Console.WriteLine("Bot started.");
            Console.WriteLine("Press Ctrl+C to exit...");

            var sw = Stopwatch.StartNew();

            var dispatcher = new MessageDispatcher();

            dispatcher.Publish(new RepeatingMessage(GetMillisecondsInMinutes(15), "I'm a bot!"));

            var connectedClients = ConnectClients();

            while(true)
            {
                dispatcher.CheckMessages((int) sw.ElapsedMilliseconds);

                while (dispatcher.TryDequeueMessage(out var message))
                {
                    foreach (var c in connectedClients)
                    {
                        c.SendMessage(message);
                    }
                }

                Thread.Sleep(500);
            }
        }

        private static long GetMillisecondsInMinutes(int i)
        {
            return i * 60 * 1000;
        }

        private static List<IChatClient> ConnectClients()
        {
            var clients = new List<IChatClient>
            {
                new ConsoleChatClient(),
                new TwitchChatClient(_config["twitch:username"], _config["twitch:oauth"])
            };

            Thread.Sleep(2000);

            return clients;
        }
    }
}
