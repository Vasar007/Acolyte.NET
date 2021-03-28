using System;

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
        /// Function to discard elements of type <<typeparamref name="TElement" />.
        /// </summary>
        public static Action<TElement> Instance { get; } = _ => { };
    }
}
