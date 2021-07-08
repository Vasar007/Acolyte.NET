using System;
using System.Threading.Tasks;

namespace Acolyte.Functions
{
    /// <summary>
    /// Helper class that provides static action for discarding.
    /// </summary>
    /// <typeparam name="TElement">
    /// The type of the elements to discard.
    /// </typeparam>
    public sealed class DiscardAction<TElement>
    {
        /// <summary>
        /// Action to discard elements of type <typeparamref name="TElement" />.
        /// </summary>
        public static Action<TElement> Instance { get; } =
            _ => { };

        /// <summary>
        /// Action to discard elements of type <typeparamref name="TElement" />.
        /// The second parameter of the action represents the index of the element.
        /// </summary>
        public static Action<TElement, int> InstanceWithIndex { get; } =
            (_, _) => { };

        /// <summary>
        /// "Asynchronous" action to discard elements of type <typeparamref name="TElement" />.
        /// It always returns <see cref="Task.CompletedTask" />.
        /// </summary>
        public static Func<TElement, Task> InstanceAsync { get; } =
            _ => Task.CompletedTask;

        /// <summary>
        /// "Asynchronous" action to discard elements of type <typeparamref name="TElement" />.
        /// The second parameter of the action represents the index of the element.
        /// It always returns <see cref="Task.CompletedTask" />.
        /// </summary>
        public static Func<TElement, int, Task> InstanceWithIndexAsync { get; } =
            (_, _) => Task.CompletedTask;
    }

    /// <summary>
    /// Helper class that provides static action for discarding and returning custom type.
    /// </summary>
    /// <typeparam name="TElement1">The first type of the elements to discard.</typeparam>
    /// <typeparam name="TElement2">The second type of the elements to discard.</typeparam>
    public sealed class DiscardAction<TElement1, TElement2>
    {
        /// <summary>
        /// Action to discard pair of elements of type <typeparamref name="TElement1" /> and
        /// <typeparamref name="TElement2" />.
        /// </summary>
        public static Action<TElement1, TElement2> Instance { get; } =
            (_, _) => { };

        /// <summary>
        /// Action to discard pair of elements of type <typeparamref name="TElement1" /> and
        /// <typeparamref name="TElement2" />.
        /// The third parameter of the action represents the index of the pair.
        /// </summary>
        public static Action<TElement1, TElement2, int> InstanceWithIndex { get; } =
            (_, _, _) => { };

        /// <summary>
        /// "Asynchronous" action to discard elements of type <typeparamref name="TElement1" /> and
        /// <typeparamref name="TElement2" />.
        /// It always returns <see cref="Task.CompletedTask" />.
        /// </summary>
        public static Func<TElement1, TElement2, Task> InstanceAsync { get; } =
            (_, _) => Task.CompletedTask;

        /// <summary>
        /// "Asynchronous" action to discard elements of type <typeparamref name="TElement1" /> and
        /// <typeparamref name="TElement2" />.
        /// The fourth parameter of the action represents the index of the element.
        /// It always returns <see cref="Task.CompletedTask" />.
        /// </summary>
        public static Func<TElement1, TElement2, int, Task> InstanceWithIndexAsync { get; } =
            (_, _, _) => Task.CompletedTask;
    }

    /// <summary>
    /// Helper class that provides static action for discarding pair and returning custom type.
    /// </summary>
    /// <typeparam name="TElement1">The first type of the elements to discard.</typeparam>
    /// <typeparam name="TElement2">The second type of the elements to discard.</typeparam>
    /// <typeparam name="TElement3">The third type of the elements to discard.</typeparam>
    public sealed class DiscardAction<TElement1, TElement2, TElement3>
    {
        /// <summary>
        /// Action to discard pair of elements of type <typeparamref name="TElement1" />,
        /// <typeparamref name="TElement2" /> and <typeparamref name="TElement3" />.
        /// </summary>
        public static Action<TElement1, TElement2, TElement3> Instance { get; } =
            (_, _, _) => { };

        /// <summary>
        /// Action to discard pair of elements of type <typeparamref name="TElement1" />,
        /// <typeparamref name="TElement2" /> and <typeparamref name="TElement3" />.
        /// The fourth parameter of the action represents the index of the pair.
        /// </summary>
        public static Action<TElement1, TElement2, TElement3, int> InstanceWithIndex { get; } =
            (_, _, _, _) => { };

        /// <summary>
        /// "Asynchronous" action to discard elements of type <typeparamref name="TElement1" />,
        /// <typeparamref name="TElement2" /> and <typeparamref name="TElement3" />.
        /// It always returns <see cref="Task.CompletedTask" />.
        /// </summary>
        public static Func<TElement1, TElement2, TElement3, Task> InstanceAsync { get; } =
            (_, _, _) => Task.CompletedTask;

        /// <summary>
        /// "Asynchronous" action to discard elements of type <typeparamref name="TElement1" />,
        /// <typeparamref name="TElement2" /> and <typeparamref name="TElement3" />.
        /// The fourth parameter of the action represents the index of the element.
        /// It always returns <see cref="Task.CompletedTask" />.
        /// </summary>
        public static Func<TElement1, TElement2, TElement3, int, Task> InstanceWithIndexAsync { get; } =
            (_, _, _, _) => Task.CompletedTask;
    }
}
