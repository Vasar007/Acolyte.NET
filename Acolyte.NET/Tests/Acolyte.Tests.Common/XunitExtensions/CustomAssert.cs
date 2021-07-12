using Acolyte.Common;
using Xunit.Sdk;

namespace Xunit
{
    public static class CustomAssert
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

        public static void False(Reasonable<bool> value)
        {
            if (!value) return;

            Fail(value.ReasonString);
        }
    }
}
