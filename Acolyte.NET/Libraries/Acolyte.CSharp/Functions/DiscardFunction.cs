using System;
using System.Threading.Tasks;

namespace Acolyte.Functions
{
    /// <summary>
    /// Helper class that provides static function for discarding.
    /// </summary>
    /// <typeparam name="TElement">
    /// The type of the elements to discard.
    /// </typeparam>
    public sealed class DiscardFunction<TElement>
    {
        /// <summary>
        /// Action to discard elements of type <typeparamref name="TElement" />.
        /// </summary>
        public static Action<TElement> Action { get; } = _ => { };

        /// <summary>
        /// Action to discard elements of type <typeparamref name="TElement" />.
        /// The second parameter of the function represents the index of the element.
        /// </summary>
        public static Action<TElement, int> ActionWithIndex { get; } = (_, _) => { };

        /// <summary>
        /// Function to discard elements of type <typeparamref name="TElement" />.
        /// </summary>
        public static Func<TElement, TElement?> Func { get; } = _ => default;

        /// <summary>
        /// Function to discard elements of type <typeparamref name="TElement" />.
        /// The second parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement, int, TElement?> FuncWithIndex { get; } = (_, _) => default;

        /// <summary>
        /// "Asynchronous" function to discard elements of type <typeparamref name="TElement" />.
        /// </summary>
        public static Func<TElement, Task<TElement?>> FuncAsync { get; } =
            _ => Task.FromResult(default(TElement));

        /// <summary>
        /// "Asynchronous" function to discard elements of type <typeparamref name="TElement" />.
        /// The second parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement, int, Task<TElement?>> FuncWithIndexAsync { get; } =
            (_, _) => Task.FromResult(default(TElement));
    }

    /// <summary>
    /// Helper class that provides static function for discarding and returning custom type.
    /// </summary>
    /// <typeparam name="TElement">The type of the elements to discard.</typeparam>
    /// <typeparam name="TReturn">
    /// Meaning depends on the properties. For properties with function it is the type to return
    /// default value of. For properties with action it is the type of the second element to
    /// discard.
    /// </typeparam>
    public sealed class DiscardFunction<TElement, TReturn>
    {
        /// <summary>
        /// Action to discard pair of elements of type <typeparamref name="TElement" /> and
        /// <typeparamref name="TReturn" />.
        /// </summary>
        public static Action<TElement, TReturn> Action { get; } = (_, _) => { };

        /// <summary>
        /// Action to discard pair of elements of type <typeparamref name="TElement" /> and
        /// <typeparamref name="TReturn" />.
        /// The third parameter of the function represents the index of the pair.
        /// </summary>
        public static Action<TElement, TReturn, int> ActionWithIndex { get; } = (_, _, _) => { };

        /// <summary>
        /// Function to discard elements of type <typeparamref name="TElement" /> and return
        /// default value of type <typeparamref name="TReturn" />.
        /// </summary>
        public static Func<TElement, TReturn?> Func { get; } = _ => default;

        /// <summary>
        /// Function to discard elements of type <typeparamref name="TElement" /> and return
        /// default value of type <typeparamref name="TReturn" />.
        /// The second parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement, int, TReturn?> FuncWithIndex { get; } = (_, _) => default;

        /// <summary>
        /// "Asynchronous" function to discard elements of type <typeparamref name="TElement" /> and
        /// return task with default value of type <typeparamref name="TReturn" />.
        /// </summary>
        public static Func<TElement, Task<TReturn?>> FuncAsync { get; } =
            _ => Task.FromResult(default(TReturn));

        /// <summary>
        /// "Asynchronous" function to discard elements of type <typeparamref name="TElement" /> and
        /// return task with default value of type <typeparamref name="TReturn" />.
        /// The second parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement, int, Task<TReturn?>> FuncWithIndexAsync { get; } =
            (_, _) => Task.FromResult(default(TReturn));
    }

    /// <summary>
    /// Helper class that provides static function for discarding pair and returning custom type.
    /// </summary>
    /// <typeparam name="TElement1">The first type of the elements to discard.</typeparam>
    /// <typeparam name="TElement2">The second type of the elements to discard.</typeparam>
    /// <typeparam name="TReturn">The type to return default value of.</typeparam>
    public sealed class DiscardFunction<TElement1, TElement2, TReturn>
    {
        /// <summary>
        /// Function to discard pair of elements of type <typeparamref name="TElement1" />
        /// <typeparamref name="TElement2"/> and return default value of type
        /// <typeparamref name="TReturn" />.
        /// </summary>
        public static Func<TElement1, TElement2, TReturn?> Func { get; } = (_, _) => default;

        /// <summary>
        /// Function to discard pair of elements of type <typeparamref name="TElement1" />
        /// <typeparamref name="TElement2"/> and return default value of type
        /// <typeparamref name="TReturn" />.
        /// The third parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement1, TElement2, int, TReturn?> FuncWithIndex { get; } = (_, _, _) => default;

        /// <summary>
        /// "Asynchronous" function to discard pair of elements of type <typeparamref name="TElement1" />
        /// <typeparamref name="TElement2"/> and return default value of type
        /// <typeparamref name="TReturn" />.
        /// </summary>
        public static Func<TElement1, TElement2, Task<TReturn?>> FuncAsync { get; } =
            (_, _) => Task.FromResult(default(TReturn));

        /// <summary>
        /// "Asynchronous" function to discard pair of elements of type <typeparamref name="TElement1" />
        /// <typeparamref name="TElement2"/> and return default value of type
        /// <typeparamref name="TReturn" />.
        /// The third parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement1, TElement2, int, Task<TReturn?>> FuncWithIndexAsync { get; } =
            (_, _, _) => Task.FromResult(default(TReturn));
    }
}
