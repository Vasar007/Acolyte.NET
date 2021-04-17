using Acolyte.Common;
using Xunit.Sdk;

namespace Xunit
{
    internal static class CustomAssert
    {
        public static void Fail(string message)
        {
            throw new XunitException(message);
        }

        public static void True(Reasonable<bool> value)
        {
            if (value) return;

            Fail(value.ReasonString);
        }
    }
}
