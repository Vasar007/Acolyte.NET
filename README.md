# Acolyte.NET

![Acolyte.NET icon](Media/AcolyteIcon.png "Acolyte.NET icon")

Acolyte.NET is a helper library with a lot of useful classes and extension methods that you need in your everyday work.
So, do not reinvent the wheel, use this library instead!

[![nuget](https://img.shields.io/nuget/v/Acolyte.NET.svg)](https://www.nuget.org/packages/Acolyte.NET)
[![License](https://img.shields.io/hexpm/l/plug.svg)](https://github.com/Vasar007/Acolyte.NET/blob/master/LICENSE)

## Build and Release Status

|                    | Stable (master)                                                                                  | Pre-release (develop)                                                                  |
| -----------------: | :----------------------------------------------------------------------------------------------: | :------------------------------------------------------------------------------------: |
| **Build**          | ![AppVeyor branch](https://img.shields.io/appveyor/build/Vasar007/acolyte-net/master)            | ![AppVeyor branch](https://img.shields.io/appveyor/build/Vasar007/acolyte-net/develop) |
| **GitHub Release** | ![GitHub release (latest by date)](https://img.shields.io/github/v/release/Vasar007/Acolyte.NET) | —                                                                                      |

## Installation

Install [NuGet package](https://www.nuget.org/packages/Acolyte.NET).

## About library

Library contains classes, helper static classes and many extension methods. Each of these is divided into one of the following categories.

<details>
<summary><strong>Table of content [click to expand]</strong></summary>
<p>

### Assertions (C#)

- [ThrowsExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Assertions/ThrowsExtensions.cs) — contains extension methods to check certain conditions.

### Collections (C#)

- [AsyncEnumerableExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Collections/AsyncEnumerableExtensions) — contains useful methods to work with `async` enumerable items (`IAsyncEnumerable` was introduced in C# 8.0);
- [ConcurrentHashSet](Acolyte.NET/Libraries/Acolyte.CSharp/Collections/ConcurrentHashSet.cs) — represents a thread-safe set of values;
- [DictionaryExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Collections/DictionaryExtensions.cs) — contains extension methods to simplify work with associative collections;
- [EnumerableExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Collections/EnumerableExtensions.cs) – extends `LINQ` methods to work with enumerable items;
- [InverseComparer](Acolyte.NET/Libraries/Acolyte.CSharp/Collections/InverseComparer.cs) — allows to invert every object of `IComparer` type;
- [IHaveCreationTime](Acolyte.NET/Libraries/Acolyte.CSharp/Collections/IHaveCreationTime.cs) — provides read-only `DateTime` property to use by `TimeBasedConcurrentDictionary`;
- [TimeBasedConcurrentDictionary](Acolyte.NET/Libraries/Acolyte.CSharp/Collections/TimeBasedConcurrentDictionary.cs) — extends the standard `ConcurrentDictionary` class with time-based logic (this collection cleans up expired objects when calling any method).

### Common (C#)

- [Constants](Acolyte.NET/Libraries/Acolyte.CSharp/Common/Constants.cs) — provides some constants (say “No” to magic numbers and strings);
- [EnumExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Common/EnumExtensions.cs) — contains extension methods for enumeration values;
- [EnumHelper](Acolyte.NET/Libraries/Acolyte.CSharp/Common/EnumHelper.cs) — contains common logic to work with enumeration values (like static methods in the standard `Enum` class);
- [GuidExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Common/GuidExtensions.cs) — contains extension methods for `Guid` values;
- [IClonable`<`T`>`](Acolyte.NET/Libraries/Acolyte.CSharp/Common/IClonableT.cs) — provides generic interface to clone objects (the standard `IClonable` interface provides `Clone` method with the `object` return type);
- [MathHelper](Acolyte.NET/Libraries/Acolyte.CSharp/Common/MathHelper.cs) — provides a set of useful mathematic methods;
- [Maybe](Acolyte.NET/Libraries/Acolyte.CSharp/Common/Maybe.cs) — represents similar interface to `Nullable` struct but for reference types;
- [TypeExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Common/TypeExtensions.cs) — contains extension methods for `Type` class;

#### Bits (C#)

- [BitConverterExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Common/Bits/BitConverterExtensions.cs) — contains extension methods to work with bits;

#### Monads (C#)

- [MonadExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Common/Monads/MonadExtensions.cs) — provides a set of monadic functions (simplify using functional style in C#);

### Data (C#)

- [DataReaderExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Data/DataReaderExtensions.cs) — contains extension methods for `IDataReader` class;

### Exceptions (C#)

- [IUnhandledExceptionHandler](Acolyte.NET/Libraries/Acolyte.CSharp/Exceptions/IUnhandledExceptionHandler.cs) — provides interface for exception handler used by `AppDomainUnhandledExceptionManager`;
- [AppDomainUnhandledExceptionManager](Acolyte.NET/Libraries/Acolyte.CSharp/Exceptions/AppDomainUnhandledExceptionManager.cs) — allows to set up exception handlers when an exception is not caught;
- [ExceptionsExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Exceptions/ExceptionsExtensions.cs) — contains extension methods to work with exceptions;
- [ExceptionsHelper](Acolyte.NET/Libraries/Acolyte.CSharp/Exceptions/ExceptionsHelper.cs) — provides methods to process or transform exceptions;
- [MultipleArgumentException](Acolyte.NET/Libraries/Acolyte.CSharp/Exceptions/MultipleArgumentException.cs) — represents the exception that is thrown when several arguments provided to a method are not valid;
- [NotFoundException](Acolyte.NET/Libraries/Acolyte.CSharp/Exceptions/NotFoundException.cs) — represents the exception that is thrown when object is not found;
- [TaskFaultedException](Acolyte.NET/Libraries/Acolyte.CSharp/Exceptions/TaskFaultedException.cs) — represents the exception that is thrown when processing method encounters task in faulted state.

### IO (C#)

- [StreamExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/IO/StreamExtensions.cs) — contains extension methods to work with streams.

### Threading (C#)

- [NoneResult](Acolyte.NET/Libraries/Acolyte.CSharp/Threading/NoneResult.cs) — represents an object with no result (used for track `Task` objects);
- [ResultOrException](Acolyte.NET/Libraries/Acolyte.CSharp/Threading/ResultOrException.cs) — represents an object with result or exception value from completed tasks;
- [TaskExtensions](Acolyte.NET/Libraries/Acolyte.CSharp/Threading/TaskExtensions.cs) — contains extension methods to work with tasks;
- [TaskHelper](Acolyte.NET/Libraries/Acolyte.CSharp/Threading/TaskHelper.cs) — contains common logic to work with tasks (like static methods in the standard `Task` class);
- [ThreadHelper](Acolyte.NET/Libraries/Acolyte.CSharp/Threading/ThreadHelper.cs) — contains additional logic to work with `Thread` class;
- [ThreadPoolHelper](Acolyte.NET/Libraries/Acolyte.CSharp/Threading/ThreadPoolHelper.cs) — contains additional logic to work with `ThreadPool` class.

### XML (C#)

- [XDocumentParser](Acolyte.NET/Libraries/Acolyte.CSharp/XML/XDocumentParser.cs) — represents a XML document parser;
- [XmlHelper](Acolyte.NET/Libraries/Acolyte.CSharp/XML/XmlHelper.cs) — provides serialization and deserialization methods to work with XML.

### Functional (F#)

- [Throw](Acolyte.NET/Libraries/Acolyte.FSharp/Functional/Throw.fs) — represents F#-style usage of some assertion extensions;
- [Utils](Acolyte.NET/Libraries/Acolyte.FSharp/Functional/Utils.fs) — provides useful methods to work with F# values;
- [SeqEx](Acolyte.NET/Libraries/Acolyte.FSharp/Functional/SeqEx.fs) — contains additional methods to work with `seq` (i.e. `IEnumerable<T>`).

</p>
</details>

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
