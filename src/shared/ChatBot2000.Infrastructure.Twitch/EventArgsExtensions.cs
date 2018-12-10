using ChatBot2000.Core;
using TwitchLib.Client.Events;

namespace ChatBot2000.Infrastructure.Twitch
{
    public static class EventArgsExtensions
    {
        public static CommandReceivedEventArgs ToCommandReceivedEventArgs(this OnChatCommandReceivedArgs src)
        {
            var commandReceivedEventArgs = new CommandReceivedEventArgs
            {
                CommandWord = src.Command.CommandText,
            };

            return commandReceivedEventArgs;
        }
    }
}
