using System;
using Acolyte.Common;

namespace Acolyte.Assertions
{
    public static partial class ThrowsExtensions
    {
        /// <summary>
        /// Checks if the <see cref="Guid" /> is empty.
        /// </summary>
        /// <param name="guid"><see cref="Guid" /> to check.</param>
        /// <param name="paramName">
        /// Name of the parameter for error message. Use operator <see langword="nameof" /> to get
        /// proper parameter name.
        /// </param>
        /// <returns>The original guid.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="paramName" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="guid" /> presents empty guid.
        /// </exception>
        public static Guid ThrowIfEmpty(this Guid guid, string paramName)
        {
            paramName.ThrowIfNull(nameof(paramName));

            if (guid.IsEmpty())
            {
                throw new ArgumentException($"{paramName} is empty.", paramName);
            }

            return guid;
        }
    }
}
