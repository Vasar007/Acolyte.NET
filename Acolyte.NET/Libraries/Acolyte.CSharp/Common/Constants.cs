namespace Acolyte.Common
{
    /// <summary>
    /// Provides some constants (say "No" to magic numbers and strings).
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Represents a commonly-used index value if the item is not found in the collection
        /// (index -1).
        /// </summary>
        public static int NotFoundIndex { get; } = -1;

        /// <summary>
        /// Represents a zero-based index value of the first item in the collection (index 0).
        /// </summary>
        public static int FirstIndex { get; } = 0;
    }
}
