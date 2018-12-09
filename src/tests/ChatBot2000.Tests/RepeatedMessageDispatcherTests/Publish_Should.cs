using ChatBot2000.Core;
using NUnit.Framework;
using System;

namespace ChatBot2000.Tests.RepeatedMessageDispatcherTests
{
    [TestFixture]
    public class Publish_Should
    {
        [Test]
        public void AddRepeatingMessageToQueue_GivenDelay()
        {
            var sut = new TriggeredMessageDispatcher();

            var repeatedMessage = new RepeatingMessage
            {
                RepeatEvery = 15,
                Message = "This is an automated message! This message will repeat."
            };

            sut.Publish(repeatedMessage);

            Assert.Contains(repeatedMessage, sut.MessagesToDispatch);
        }
    }
}
