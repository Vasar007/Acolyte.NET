using System;
using System.Runtime.ExceptionServices;
using Acolyte.Assertions;

namespace Acolyte.Exceptions
{
    public static class ExceptionsExtensions
    {
        public static Exception Rethrow(this Exception exception)
        {
            exception.ThrowIfNull(nameof(exception));

            ExceptionDispatchInfo dispatchInfo = Dispatch(exception);
            dispatchInfo.Throw();

            return exception; // An unreachable code for return value.
        }

        public static ExceptionDispatchInfo Dispatch(this Exception exception)
        {
            exception.ThrowIfNull(nameof(exception));

            return ExceptionDispatchInfo.Capture(exception);
        }
    }
}
