using ChatBot2000.Core;
using ChatBot2000.Core.Messaging;
using ChatBot2000.Tests.Fakes.Messages;
using NUnit.Framework;

namespace ChatBot2000.Tests.MessageDispatcherTests
{
    [TestFixture]
    public class DequeueMessage_Should
    {
        [Test]
        public void ReturnFalse_GivenNoMessagesInQueue()
        {
            var sut = new MessageDispatcher();

            //sut.Publish(new AlwaysReadyMessage());
            //sut.CheckMessages(10);
            var result = sut.TryDequeueMessage(out _);

            Assert.IsFalse(result);

        }

        [Test]
        public void ReturnTrue_AfterReturningMessage()
        {
            var sut = new MessageDispatcher();

            sut.Publish(new AlwaysReadyMessage());
            sut.CheckMessages(10);

            var result = sut.TryDequeueMessage(out _);

            Assert.IsTrue(result);

        }

        [Test]
        public void ReturnQueuedMessage()
        {
            var sut = new MessageDispatcher();

            var alwaysReadyMessage = new AlwaysReadyMessage();
            sut.Publish(alwaysReadyMessage);
            sut.CheckMessages(10);

            sut.TryDequeueMessage(out var actual);

            Assert.AreEqual(alwaysReadyMessage.Message, actual);

        }

        [Test]
        public void PopOffMessageFromQueue_AfterReturningMessage()
        {
            var sut = new MessageDispatcher();

            sut.Publish(new AlwaysReadyMessage());
            sut.CheckMessages(10);

            sut.TryDequeueMessage(out _);

            Assert.AreEqual(0, sut.QueuedMessages.Count);

        }
    }
}
