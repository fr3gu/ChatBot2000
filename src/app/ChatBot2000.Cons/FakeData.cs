using System.Collections.Generic;
using ChatBot2000.Core.Interfaces;
using ChatBot2000.Core.Messaging;

namespace ChatBot2000.Cons
{
    public class FakeData
    {
        private readonly IRepository _repository;

        public FakeData(IRepository repository)
        {
            _repository = repository;
        }

        private static long GetMillisecondsFromMinutes(int i)
        {
            return i * 60 * 1000;
        }

        private static List<RepeatingMessage> GetRepeatingMessages()
        {
            var automatedMessages = new List<RepeatingMessage>
            {
                new RepeatingMessage(3000, "Yo! Nice to see ya. Plz follow. <3 Peace!", DataItemStatus.Active),
                new RepeatingMessage(2000, "Greta", DataItemStatus.Draft),
                new RepeatingMessage(1000, "Ulla", DataItemStatus.Disabled),
            };

            return automatedMessages;
        }

        private static List<SimpleResponseMessage> GetICommandMessages()
        {
            return new List<SimpleResponseMessage>
            {
                new SimpleResponseMessage("coins", "Coins?!?! I think you meant !points", DataItemStatus.Active),
            };

        }

        public void Initialize()
        {
            _repository.Create(GetRepeatingMessages());

            _repository.Create(GetICommandMessages());
        }
    }
}
