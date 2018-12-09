using System.Collections.Generic;
using System.Text;
using ChatBot2000.Core;
using NUnit.Framework;

namespace ChatBot2000.Tests.RepeatingMessageTest
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
        public void ReturnFalseTrue_WhenTimePasseedEqualToDelay()
        {
            var message = new RepeatingMessage(60, "");

            Assert.IsTrue(message.IsTimeToDispatch(60));
        }
    }
}
