using System;

namespace Acolyte.Common
{
    /// <summary>
    /// Defines extension methods for <see cref="Guid" /> struct.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Compare guid with the empty guid without throwing exception.
        /// </summary>
        /// <param name="guid">Guid to compare.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="guid" /> equals to empty guid, <c>falst</c> otherwise.
        /// </returns>
        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }
    }
}
