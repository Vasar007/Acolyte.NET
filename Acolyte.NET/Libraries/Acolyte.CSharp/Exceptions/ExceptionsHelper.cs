using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using Acolyte.Assertions;

namespace Acolyte.Exceptions
{
    /// <summary>
    ///  Provides methods to process or transform exceptions.
    /// </summary>
    public static class ExceptionsHelper
    {
        public static Exception UnwrapAggregateExceptionIfSingle(
            AggregateException aggregateException)
        {
            aggregateException.ThrowIfNull(nameof(aggregateException));

            return aggregateException.InnerExceptions.FirstOrDefault() ?? aggregateException;
        }

        public static Exception UnwrapAndThrow(AggregateException aggregateException)
        {
            aggregateException.ThrowIfNull(nameof(aggregateException));

            Exception firstException = UnwrapAggregateExceptionIfSingle(aggregateException);
            var dispatchInfo = ExceptionDispatchInfo.Capture(firstException);
            dispatchInfo.Throw();

            return firstException;
        }
    }
}
