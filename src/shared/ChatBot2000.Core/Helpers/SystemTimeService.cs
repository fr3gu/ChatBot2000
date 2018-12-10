using System;
using System.Diagnostics.CodeAnalysis;

namespace ChatBot2000.Core.Helpers
{

    [ExcludeFromCodeCoverage]
    public static class SystemTimeService
    {
        public static Func<DateTime> Now = () => DateTime.Now;
        public static Func<DateTime> UtcNow = () => DateTime.UtcNow;
    }
}
