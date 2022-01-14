using System;

namespace Acolyte.Common
{
    /// <summary>
    /// Provides generic interface to clone objects.
    /// </summary>
    /// <typeparam name="T">The type of the object to clone.</typeparam>
    public interface ICloneable<T> : ICloneable
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        new T Clone();
    }
}
