using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acolyte.Common;

namespace Acolyte.Examples.ConsoleApp
{
    internal static class Program
    {
        private sealed class StringGenerator
        {
            private readonly IReadOnlyList<string> _data;

            private int _counter;


            public StringGenerator()
            {
                _data = new[] { "a", "ab", "abc", "ac" };
                _counter = 0;
            }

            public string Generate()
            {
                if (_counter < _data.Count)
                {
                    return _data[_counter++];
                }

                _counter = 0;
                return _data[_counter++];
            }
        }


        private static async Task<int> Main()
        {
            try
            {
                Console.WriteLine("Console application started.");

                var generator = new StringGenerator();
                var source = Samples.GenerateCollection(() => generator.Generate(), count: 10);
                var items = Samples.GetItemsByPattern(source, pattern: "ab");
                Samples.OutputCollection(items);

                var ints = Enumerable.Range(start: 1, count: 15);
                var distance = Samples.DistanceBetweenMinAndMax(ints);
                Console.WriteLine($"Distance between min and max: {distance.ToString()}.");

                await Samples.ExecuteAllTasksSafe(
                    Task.Run(() => SomeAsyncStuff(0)),
                    Task.Run(() => SomeAsyncStuff(1)),
                    Task.Run(() => SomeAsyncStuff(2))
                );

                return ExitCodes.Success;
            }
            catch (Exception ex)
            {
                string exceptionMessage = $"Exception occurred in {nameof(Main)} method. " +
                                          $"{Environment.NewLine}{ex}";
                Console.WriteLine(exceptionMessage);

                return ExitCodes.Fail;
            }
            finally
            {
                Console.WriteLine("Console application stopped.");
                Console.WriteLine("Press any key to close this window...");
                Console.ReadKey();
            }
        }

        private static async Task SomeAsyncStuff(int id)
        {
            await Task.Delay(delay: TimeSpan.FromSeconds(1));

            throw new Exception($"Something went wrong in task '{id.ToString()}'.");
        }
    }
}
