namespace Acolyte.Common
{
    /// <summary>
    /// Provides some constants (say "No" to magic numbers and strings).
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Represents a commonly-used index value if the item is not found in the collection.
        /// </summary>
        public static int NotFoundIndex { get; } = -1;

        /// <summary>
        /// Standard message when a sequence contains no elements.
        /// </summary>
        internal static string NoElementsErrorMessage { get; } = "Sequence contains no elements.";
    }
}
