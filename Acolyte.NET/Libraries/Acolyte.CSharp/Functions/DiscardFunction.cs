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
        /// Function to discard elements of type <typeparamref name="TElement" />.
        /// </summary>
        public static Func<TElement, TElement?> Func { get; } = _ => default;

        /// <summary>
        /// "Asynchronous" function to discard elements of type <typeparamref name="TElement" />.
        /// </summary>
        public static Func<TElement, Task<TElement?>> FuncAsync { get; } =
            _ => Task.FromResult(default(TElement));
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
        public static Func<TElement, TReturn?> Func { get; } = _ => default;

        /// <summary>
        /// "Asynchronous" function to discard elements of type <typeparamref name="TElement" /> and
        /// return task with default value of type <typeparamref name="TReturn" />.
        /// </summary>
        public static Func<TElement, Task<TReturn?>> FuncAsync { get; } =
            _ => Task.FromResult(default(TReturn));
    }
}
