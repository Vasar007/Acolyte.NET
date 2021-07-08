using System;
using System.Threading.Tasks;

namespace Acolyte.Functions
{
    /// <summary>
    /// Helper class that provides static function for discarding.
    /// </summary>
    /// <typeparam name="TElement">The type of the elements to discard.</typeparam>
    public sealed class DiscardFunction<TElement>
    {
        /// <summary>
        /// Function to discard elements of type <typeparamref name="TElement" />.
        /// </summary>
        public static Func<TElement, TElement?> Instance { get; } =
            _ => default;

        /// <summary>
        /// Function to discard elements of type <typeparamref name="TElement" />.
        /// The second parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement, int, TElement?> InstanceWithIndex { get; } =
            (_, _) => default;

        /// <summary>
        /// "Asynchronous" function to discard elements of type <typeparamref name="TElement" />.
        /// </summary>
        public static Func<TElement, Task<TElement?>> InstanceAsync { get; } =
            _ => Task.FromResult(default(TElement));

        /// <summary>
        /// "Asynchronous" function to discard elements of type <typeparamref name="TElement" />.
        /// The second parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement, int, Task<TElement?>> InstanceWithIndexAsync { get; } =
            (_, _) => Task.FromResult(default(TElement));
    }

    /// <summary>
    /// Helper class that provides static function for discarding and returning custom type.
    /// </summary>
    /// <typeparam name="TElement">The type of the elements to discard.</typeparam>
    /// <typeparam name="TReturn">The type to return default value of.</typeparam>
    public sealed class DiscardFunction<TElement, TReturn>
    {
        /// <summary>
        /// Function to discard elements of type <typeparamref name="TElement" /> and return
        /// default value of type <typeparamref name="TReturn" />.
        /// </summary>
        public static Func<TElement, TReturn?> Instance { get; } =
            _ => default;

        /// <summary>
        /// Function to discard elements of type <typeparamref name="TElement" /> and return
        /// default value of type <typeparamref name="TReturn" />.
        /// The second parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement, int, TReturn?> InstanceWithIndex { get; } =
            (_, _) => default;

        /// <summary>
        /// "Asynchronous" function to discard elements of type <typeparamref name="TElement" /> and
        /// return task with default value of type <typeparamref name="TReturn" />.
        /// </summary>
        public static Func<TElement, Task<TReturn?>> InstanceAsync { get; } =
            _ => Task.FromResult(default(TReturn));

        /// <summary>
        /// "Asynchronous" function to discard elements of type <typeparamref name="TElement" /> and
        /// return task with default value of type <typeparamref name="TReturn" />.
        /// The second parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement, int, Task<TReturn?>> InstanceWithIndexAsync { get; } =
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
        /// Function to discard pair of elements of type <typeparamref name="TElement1" />,
        /// <typeparamref name="TElement2"/> and return default value of type
        /// <typeparamref name="TReturn" />.
        /// </summary>
        public static Func<TElement1, TElement2, TReturn?> Instance { get; } =
            (_, _) => default;

        /// <summary>
        /// Function to discard pair of elements of type <typeparamref name="TElement1" />,
        /// <typeparamref name="TElement2"/> and return default value of type
        /// <typeparamref name="TReturn" />.
        /// The third parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement1, TElement2, int, TReturn?> InstanceWithIndex { get; } =
            (_, _, _) => default;

        /// <summary>
        /// "Asynchronous" function to discard pair of elements of type
        /// <typeparamref name="TElement1" />, <typeparamref name="TElement2"/> and return default
        /// value of type <typeparamref name="TReturn" />.
        /// </summary>
        public static Func<TElement1, TElement2, Task<TReturn?>> InstanceAsync { get; } =
            (_, _) => Task.FromResult(default(TReturn));

        /// <summary>
        /// "Asynchronous" function to discard pair of elements of type
        /// <typeparamref name="TElement1" />, <typeparamref name="TElement2"/> and return default
        /// value of type <typeparamref name="TReturn" />.
        /// The third parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement1, TElement2, int, Task<TReturn?>> InstanceWithIndexAsync { get; } =
            (_, _, _) => Task.FromResult(default(TReturn));
    }

    /// <summary>
    /// Helper class that provides static function for discarding pair and returning custom type.
    /// </summary>
    /// <typeparam name="TElement1">The first type of the elements to discard.</typeparam>
    /// <typeparam name="TElement2">The second type of the elements to discard.</typeparam>
    /// <typeparam name="TElement3">The third type of the elements to discard.</typeparam>
    /// <typeparam name="TReturn">The type to return default value of.</typeparam>
    public sealed class DiscardFunction<TElement1, TElement2, TElement3, TReturn>
    {
        /// <summary>
        /// Function to discard pair of elements of type <typeparamref name="TElement1" />,
        /// <typeparamref name="TElement2"/>, <typeparamref name="TElement3"/>  and return
        /// default value of type <typeparamref name="TReturn" />.
        /// </summary>
        public static Func<TElement1, TElement2, TElement3, TReturn?> Instance { get; } =
            (_, _, _) => default;

        /// <summary>
        /// Function to discard pair of elements of type <typeparamref name="TElement1" />,
        /// <typeparamref name="TElement2"/> and return default value of type
        /// <typeparamref name="TReturn" />.
        /// The third parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement1, TElement2, TElement3, int, TReturn?> InstanceWithIndex { get; } =
            (_, _, _, _) => default;

        /// <summary>
        /// "Asynchronous" function to discard pair of elements of type
        /// <typeparamref name="TElement1" />, <typeparamref name="TElement2"/>,
        /// <typeparamref name="TElement3"/> and return default value of type
        /// <typeparamref name="TReturn" />.
        /// </summary>
        public static Func<TElement1, TElement2, TElement3, Task<TReturn?>> InstanceAsync { get; } =
            (_, _, _) => Task.FromResult(default(TReturn));

        /// <summary>
        /// "Asynchronous" function to discard pair of elements of type <typeparamref name="TElement1" />
        /// <typeparamref name="TElement2"/>, <typeparamref name="TElement3"/> and return
        /// default value of type <typeparamref name="TReturn" />.
        /// The third parameter of the function represents the index of the element.
        /// </summary>
        public static Func<TElement1, TElement2, TElement3, int, Task<TReturn?>> InstanceWithIndexAsync { get; } =
            (_, _, _, _) => Task.FromResult(default(TReturn));
    }
}
