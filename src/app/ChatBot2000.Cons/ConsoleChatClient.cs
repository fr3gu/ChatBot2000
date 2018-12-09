using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChatBot2000.Core.Interfaces;

namespace ChatBot2000.Cons
{
    public class ConsoleChatClient : IChatClient
    {
        public void Connect()
        {
            // Noop
            Console.WriteLine("connected...");
        }

        public void SendMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {message}");
        }
    }
}
