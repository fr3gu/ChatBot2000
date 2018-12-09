using System;
using System.Diagnostics;
using System.Threading;
using ChatBot2000.Core;
using ChatBot2000.Core.Messages;

namespace ChatBot2000.Cons
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Bot started.");
            Console.WriteLine("Press Ctrl+C to exit...");

            var sw = Stopwatch.StartNew();

            var dispatcher = new MessageDispatcher();

            dispatcher.Publish(new RepeatingMessage(5, "I'm a bot!"));

            while (true)
            {
                
                dispatcher.CheckMessages((int)sw.ElapsedMilliseconds / 1000);
                while(dispatcher.TryDequeueMessage(out var message))
                {
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {message}");
                }

                Thread.Sleep(500);
            }
        }
    }
}
