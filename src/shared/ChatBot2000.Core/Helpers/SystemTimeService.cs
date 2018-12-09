using System;

namespace ChatBot2000.Core.Helpers
{
    public static class SystemTimeService
    {
        public static Func<DateTime> Now = () => DateTime.Now;
        public static Func<DateTime> UtcNow = () => DateTime.UtcNow;
    }
}
