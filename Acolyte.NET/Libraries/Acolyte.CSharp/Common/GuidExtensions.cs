using System;

namespace Acolyte.Common
{
    /// <summary>
    /// Defines extension methods for <see cref="Guid" /> type.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Compare value with the <see cref="Guid.Empty" /> without throwing exception.
        /// </summary>
        /// <param name="guid">Value to compare.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="guid" /> equals to <see cref="Guid.Empty" />,
        /// <see langword="false" /> otherwise.
        /// </returns>
        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }
    }
}
