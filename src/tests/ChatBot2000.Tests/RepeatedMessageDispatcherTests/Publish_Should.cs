using ChatBot2000.Core;
using NUnit.Framework;
using System;

namespace ChatBot2000.Tests.RepeatedMessageDispatcherTests
{
    [TestFixture]
    public class Publish_Should
    {
        [Test]
        public void AddRepeatedMessageToQueue_GivenDelay()
        {
            var sut = new RepeatedMessageDispatcher();

            var repeatedMessage = new RepeatedMessage
            {
                Delay = 15,
                Message = "This is an automated message!"
            };

            sut.Publish(repeatedMessage);

            Assert.Contains(repeatedMessage, sut.MessagesToDispatch);
        }
    }
}
