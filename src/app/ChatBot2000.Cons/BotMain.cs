using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ChatBot2000.Core;
using ChatBot2000.Core.Data;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messaging;
using ChatBot2000.Core.Messaging.Interfaces;

namespace ChatBot2000.Cons
{
    public class BotMain
    {
        private readonly List<IChatClient> _chatClients;
        private readonly IRepository _repository;
        private readonly MessageDispatcher _dispatcher;
        private readonly CancellationTokenSource _tokenSource;
        private readonly CommandHandler _commandHandler;
        private const int REFRESH_INTERVAL = 500;

        public BotMain(List<IChatClient> chatClients, IRepository repository, CommandHandler commandHandler)
        {
            _chatClients = chatClients;
            _repository = repository;
            _commandHandler = commandHandler;
            _dispatcher = new MessageDispatcher();
            _tokenSource = new CancellationTokenSource();
        }

        public void Run()
        {
            PublishMessages();

            ConnectChatClients();

            BeginLoop();
        }

        private void ConnectChatClients()
        {
            var getUserTasks = new List<Task>();

            foreach (var chatClient in _chatClients)
            {
                getUserTasks.Add(chatClient.Connect());
            }

            Task.WhenAll(getUserTasks);
        }

        private void DisconnectChatClients()
        {
            foreach (var chatClient in _chatClients)
            {
                chatClient.SendMessage("Goodby for now! The bot has left the building");
            }

            var disconnectedTasks = new List<Task>();
            foreach (var chatClient in _chatClients)
            {
                disconnectedTasks.Add(chatClient.Disconnect());
            }

            Task.WhenAll(disconnectedTasks);

        }

        private void PublishMessages()
        {
            var messages = _repository.List(new ActiveMessagePolicy<IAutoMessage>());
            foreach (var message in messages)
            {
                _dispatcher.Publish(message);
            }

        }

        // ReSharper disable once FunctionNeverReturns
        private void BeginLoop()
        {
            var sw = Stopwatch.StartNew();
            Task.Run(() =>
            {
                while (_tokenSource.IsCancellationRequested != true)
                {
                    _dispatcher.CheckMessages((int) sw.ElapsedMilliseconds);

                    while (_dispatcher.TryDequeueMessage(out var message))
                    {
                        foreach (var c in _chatClients)
                        {
                            c.SendMessage(message);
                        }
                    }

                    Thread.Sleep(REFRESH_INTERVAL);
                }
            });
        }

        private void StopLoop()
        {

            _tokenSource.Cancel();
        }

        private static long GetMillisecondsFromMinutes(int i)
        {
            return i * 60 * 1000;
        }

        public void Stop()
        {
            StopLoop();

            //_followableSystem.StopHandlingNotifications();

            DisconnectChatClients();

            //todo check if publish messages needs to be cleaned ?
        }
    }
}
