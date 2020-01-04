# Acolyte.NET

[![nuget](https://img.shields.io/nuget/v/Acolyte.NET.svg)](https://www.nuget.org/packages/Acolyte.NET)
[![License](https://img.shields.io/hexpm/l/plug.svg)](https://github.com/Vasar007/Acolyte.NET/blob/master/LICENSE)
[![AppVeyor branch](https://img.shields.io/appveyor/ci/Vasar007/acolyte-net/master.svg)](https://ci.appveyor.com/project/Vasar007/acolyte.net)

<div style="text-align:center"><img src="Media/AcolyteIcon.png" alt="Acolyte.NET icon" title="Acolyte.NET icon" style="max-width:100%;"></div>

Acolyte.NET is a helper library with a lot of useful classes and extension methods that you need in your everyday work.
So, do not reinvent the wheel, use this library instead!

## Installation

Install [NuGet package](https://www.nuget.org/packages/Acolyte.NET).

## Usage examples

Check examples project [here](https://github.com/Vasar007/Acolyte.NET/blob/master/Examples/Acolyte.Examples).

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acolyte.Assertions;
using Acolyte.Collections;
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

            return Enumerable.Range(0, count)
                .Select(_ => generator());
        }

        internal static IEnumerable<string> GetItemsByPattern(IEnumerable<string> source,
            string pattern)
        {
            source.ThrowIfNull(nameof(source));
            pattern.ThrowIfNullOrEmpty(nameof(pattern));

            return source
                .Where(item => Regex.IsMatch(item, pattern));
        }

        internal static void OutputCollection<T>(IEnumerable<T> collection)
        {
            collection.ThrowIfNull(nameof(collection));

            Console.WriteLine($"Collection: [{collection.EnumerableToOneString()}].");
        }

        internal static int DistanceBetweenMinAndMax(IEnumerable<int> source)
        {
            source.ThrowIfNullOrEmpty(nameof(source));

            (int minValue, int maxValue) = source.MinMax();
            return maxValue - minValue;
        }

        internal static async Task ExecuteAllTasksSafe(params Task[] tasks)
        {
            IReadOnlyList<ResultOrException<NoneResult>> resultObjects =
                await TaskHelper.WhenAllResultsOrExceptions(tasks);

            IReadOnlyList<Exception> exceptions = resultObjects.UnwrapResultsOrExceptions();

            string separator = Environment.NewLine;
            const string emptyCollectionMessage = "No exceptions occurred.";
            string exceptionsToLog = exceptions.EnumerableToOneString(
                separator, emptyCollectionMessage, selector: ex => ex.ToString()
            );

            Console.WriteLine($"{separator}{exceptionsToLog}{separator}");
        }
    }
}

```

## Dependencies

Target .NET Standard is 2.1 for libraries. Version of C# is 8.0, version of F# is 4.7.

## License information

This project is licensed under the terms of the [Apache License 2.0](LICENSE).
