using ChatBot2000.Core;
using ChatBot2000.Core.Messaging;
using NUnit.Framework;

namespace ChatBot2000.Tests.MessageDispatcherTests
{
    [TestFixture]
    public class Publish_Should
    {
        [Test]
        public void AddRepeatingMessageToQueue_GivenDelay()
        {
            var sut = new MessageDispatcher();

            var repeatedMessage = new RepeatingMessage(15, "This is an automated message! This message will repeat.");

            sut.Publish(repeatedMessage);

            Assert.Contains(repeatedMessage, sut.MessagesToDispatch);
        }
    }
}
