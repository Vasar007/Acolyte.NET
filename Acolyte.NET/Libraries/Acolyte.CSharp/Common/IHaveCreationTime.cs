using System;

namespace Acolyte.Common
{
    /// <summary>
    /// Provides read-only <see cref="DateTime" /> property to store creation time in UTC format.
    /// </summary>
    public interface IHaveCreationTime
    {
        /// <summary>
        /// Creation time of the object.
        /// </summary>
        DateTime CreationTimeUtc { get; }
    }
}
