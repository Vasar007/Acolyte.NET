using System;

namespace Acolyte.Functions
{
    /// <summary>
    /// Helper class that provides static function for mapping to itself.
    /// </summary>
    /// <typeparam name="TElement">
    /// The type of the elements to map to itself.
    /// </typeparam>
    public static class IdentityFunction<TElement>
    {
        /// <summary>
        /// Function for mapping elements of type <typeparamref name="TElement" /> to itself.
        /// </summary>
        public static Func<TElement, TElement> Instance { get; } = x => x;
    }
}
