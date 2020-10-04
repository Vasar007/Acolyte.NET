using System;

namespace Acolyte.Common
{
    /// <summary>
    /// Provides read-only <see cref="DateTime" /> property to store creation time.
    /// </summary>
    public interface IHaveCreationTime
    {
        /// <summary>
        /// Creation time of the object.
        /// </summary>
        DateTime CreationTime { get; }
    }
}
