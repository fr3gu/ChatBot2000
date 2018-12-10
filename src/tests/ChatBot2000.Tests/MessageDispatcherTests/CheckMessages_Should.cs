using ChatBot2000.Core;
using ChatBot2000.Core.Messaging;
using ChatBot2000.Tests.Fakes.Messages;
using NUnit.Framework;

namespace ChatBot2000.Tests.MessageDispatcherTests
{
    [TestFixture]
    public class CheckMessages_Should
    {
        [Test]
        public void ReturnFalse_GivenNoMessagesInQueue()
        {
            var sut = new MessageDispatcher();

            var result = sut.CheckMessages(62);

            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnFalse_GivenMessagesToSendInFuture()
        {
            var sut = new MessageDispatcher();

            sut.Publish(new RepeatingMessage(120, "Hello from future!"));

            var result = sut.CheckMessages(62);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddOneMessageToTheQueue_IfTimeToDispatch()
        {
            var sut = new MessageDispatcher();

            sut.Publish(new AlwaysReadyMessage());

            sut.CheckMessages(10);

            Assert.IsNotEmpty(sut.QueuedMessages);
        }

        [Test]
        public void AddOnlyOneMessageToTheQueue_GivenManyMesages_OnlyOneReady()
        {
            var sut = new MessageDispatcher();

            sut.Publish(new AlwaysReadyMessage());
            sut.Publish(new RepeatingMessage(120, "Häpp"));
            sut.Publish(new RepeatingMessage(70, "Hupp"));

            sut.CheckMessages(10);

            Assert.AreEqual(1, sut.QueuedMessages.Count);
        }


        [Test]
        public void AddMessagesToQueueIfItsTimeToDispatch()
        {
            var sut = new MessageDispatcher();

            sut.Publish(new RepeatingMessage(60, "Hello!"));

            var result = sut.CheckMessages(62);

            Assert.IsTrue(result);
        }
    }
}
