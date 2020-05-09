using System;

namespace Acolyte.Common
{
    internal static class Error
    {
        public static Exception MoreThanOneElement()
        {
            return new InvalidOperationException(Strings.MoreThanOneElementErrorMessage);
        }

        public static Exception MoreThanOneMatch()
        {
            return new InvalidOperationException(Strings.MoreThanOneMatchErrorMessage);
        }

        public static Exception NoElements()
        {
            return new InvalidOperationException(Strings.NoElementsErrorMessage);
        }

        public static Exception NoMatch()
        {
            return new InvalidOperationException(Strings.NoMatchErrorMessage);
        }

        public static Exception ArgumentNull(string paramName)
        {
            return new ArgumentNullException(paramName);
        }

        public static Exception ArgumentOutOfRange(string paramName)
        {
            return new ArgumentOutOfRangeException(paramName);
        }

        public static Exception NotImplemented()
        {
            return new NotImplementedException();
        }

        public static Exception NotSupported()
        {
            return new NotSupportedException();
        }
    }
}
