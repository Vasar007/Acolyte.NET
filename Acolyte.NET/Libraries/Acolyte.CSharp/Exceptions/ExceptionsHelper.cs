using System;
using System.Linq;
using Acolyte.Assertions;

namespace Acolyte.Exceptions
{
    public static class ExceptionsHelper
    {
        public static Exception UnwrapAggregateExceptionIfSingle(
            AggregateException aggregateException)
        {
            aggregateException.ThrowIfNull(nameof(aggregateException));

            Exception resultException = aggregateException.InnerExceptions.Count == 1
                ? aggregateException.InnerExceptions.Single()
                : aggregateException;

            return resultException;
        }
    }
}
