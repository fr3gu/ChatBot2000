using ChatBot2000.Core.Messaging;
using NUnit.Framework;

namespace ChatBot2000.Tests.RepeatingMessageTests
{
    [TestFixture]
    public class RepeatingMessage_Should
    {
        [Test]
        public void ReturnFalse_AtInitialSetup()
        {
            var message = new RepeatingMessage(0, "");

            Assert.IsFalse(message.IsTimeToDispatch(0));
        }

        [Test]
        public void ReturnFalse_WhenTimePasseedNotEqualToDelay()
        {
            var message = new RepeatingMessage(60, "");

            Assert.IsFalse(message.IsTimeToDispatch(59));
        }

        [Test]
        public void ReturnTrue_WhenTimePasseedEqualToDelay()
        {
            var message = new RepeatingMessage(60, "");

            Assert.IsTrue(message.IsTimeToDispatch(61));
        }

        [Test]
        public void ReturnFalse_ImmediatelyAfterDispatching()
        {
            var message = new RepeatingMessage(60, "");

            message.GetMessageInstance(59);

            Assert.IsFalse(message.IsTimeToDispatch(61));
        }

    }
}
