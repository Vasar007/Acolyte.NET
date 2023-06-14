namespace Acolyte.Assertions
{
    /// <summary>
    /// Contains extension methods to check certain conditions.
    /// </summary>
    public static partial class ThrowsExtensions
    {
        /// <summary>
        /// Specifies a default value for "assertOnPureValueTypes" parameter.
        /// </summary>
        /// <remarks>
        /// "assertOnPureValueTypes" parameter allows to throw exception if object's type is pure value
        /// type ("pure" means that it is non-nullable type). It can be useful when you do not want
        /// to check "pure" value types. So, using this parameter you can catch assertion during
        /// debug and eliminate redundant checks (because non-nullable value types do not have null
        /// value at all).
        /// </remarks>
        public static bool DefaultAssertOnPureValueTypes { get; } = false;
    }
}
