using System;

namespace Acolyte.Collections
{
    /// <summary>
    /// Provides read-only <see cref="DateTime" /> property to use by
    /// <see cref="TimeBasedConcurrentDictionary{TKey, TValue}" />.
    /// </summary>
    public interface IHaveCreationTime
    {
        /// <summary>
        /// Creation time of the object.
        /// </summary>
        DateTime CreationTime { get; }
    }
}
