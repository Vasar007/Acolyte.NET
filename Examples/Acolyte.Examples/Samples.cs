using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acolyte.Assertions;
using Acolyte.Collections;
using Acolyte.Common;
using Acolyte.Threading;

namespace Acolyte.Examples
{
    internal static class Samples
    {
        internal static IEnumerable<T> GenerateCollection<T>(Func<T> generator, int count)
        {
            generator.ThrowIfNull(nameof(generator));
            count.ThrowIfValueIsOutOfRange(
                nameof(count), includedLowerBound: 1, includedUpperBound: 1000
            );

            return Enumerable
                .Range(0, count)
                .Select(_ => generator());
        }

        internal static IEnumerable<string> GetItemsByPattern(IEnumerable<string> source,
            string pattern)
        {
            source.ThrowIfNull(nameof(source));
            pattern.ThrowIfNullOrEmpty(nameof(pattern));

            return source.Where(item => Regex.IsMatch(item, pattern));
        }

        internal static void OutputCollection<T>(IEnumerable<T> collection)
        {
            collection.ThrowIfNull(nameof(collection));

            Console.WriteLine($"Collection: [{collection.ToSingleString()}].");
        }

        internal static int DistanceBetweenMinAndMax(IEnumerable<int> source)
        {
            source.ThrowIfNullOrEmpty(nameof(source));

            (int minValue, int maxValue) = source.MinMax();
            return maxValue - minValue;
        }

        internal static async Task ExecuteAllTasksSafe(params Task[] tasks)
        {
            IReadOnlyList<Result<NoneResult, Exception>> resultObjects =
                await TaskHelper.WhenAllResultsOrExceptions(tasks);

            IReadOnlyList<Exception> exceptions = resultObjects.UnwrapResultsOrExceptions();

            string separator = Environment.NewLine;
            const string emptyCollectionMessage = "No exceptions occurred.";
            string exceptionsToLog = exceptions.ToSingleString(
                emptyCollectionMessage, separator, selector: ex => ex.ToString()
            );

            Console.WriteLine($"{separator}{exceptionsToLog}{separator}");
        }
    }
}
