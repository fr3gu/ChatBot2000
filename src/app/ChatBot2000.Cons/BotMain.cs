using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ChatBot2000.Core;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messages;

namespace ChatBot2000.Cons
{
    public class BotMain
    {
        private readonly List<IChatClient> _chatClients;

        public BotMain(List<IChatClient> chatClients)
        {
            _chatClients = chatClients;
        }

        public void Run()
        {
            var messageDispatcher = new MessageDispatcher();

            PublishMessages(messageDispatcher);

            ConnectClients();

            BeginLoop(messageDispatcher);
        }

        private void ConnectClients()
        {
            var getUserTasks = new List<Task>();

            foreach (var chatClient in _chatClients)
            {
                getUserTasks.Add(chatClient.Connect());
            }

            Task.WhenAll(getUserTasks);
        }

        private void PublishMessages(MessageDispatcher dispatcher)
        {
            dispatcher.Publish(new RepeatingMessage(GetMillisecondsInMinutes(15), "I'm a bot!"));

        }

        // ReSharper disable once FunctionNeverReturns
        private void BeginLoop(MessageDispatcher dispatcher)
        {
            var sw = Stopwatch.StartNew();

            while (true)
            {
                dispatcher.CheckMessages((int)sw.ElapsedMilliseconds);

                while (dispatcher.TryDequeueMessage(out var message))
                {
                    foreach (var c in _chatClients)
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
    }
}
