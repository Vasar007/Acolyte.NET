using Xunit.Sdk;

namespace Xunit
{
    internal static class CustomAssert
    {
        public static void Fail(string message)
        {
            throw new XunitException(message);
        }
    }
}
