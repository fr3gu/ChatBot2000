using System;
using System.Threading.Tasks;
using ChatBot2000.Core;
using ChatBot2000.Core.Interfaces;

namespace ChatBot2000.Cons
{
    public class ConsoleChatClient : IChatClient
    {
        public Task Connect()
        {
            // Noop
            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {nameof(ConsoleChatClient)} connected...");
            return Task.CompletedTask;
        }

        public void SendMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {message}");
        }

        public Task Disconnect()
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {nameof(ConsoleChatClient)} disconnected...");

            return Task.CompletedTask;
        }

        public event EventHandler<CommandReceivedEventArgs> OnCommandReceived;
    }
}
