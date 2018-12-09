using System;
using System.Threading.Tasks;
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
    }
}
