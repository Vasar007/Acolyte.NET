using System;

namespace Acolyte.Common
{
    /// <summary>
    /// Defines extension methods for <see cref="Type" /> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks that <paramref name="potentialDescendant" /> is same type as
        /// <paramref name="potentialDescendant" /> or is the it subclass.
        /// </summary>
        /// <param name="potentialDescendant">Potential descendant type to check.</param>
        /// <param name="potentialBase">Potential base type to compare.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="potentialDescendant" /> is same type as
        /// <paramref name="potentialDescendant" /> or is the it subclass,
        /// <see langword="false" /> otherwise.
        /// </returns>
        public static bool IsSameOrSubclass(this Type potentialDescendant, Type potentialBase)
        {
            return potentialDescendant.IsSubclassOf(potentialBase) ||
                   potentialDescendant.Equals(potentialBase);
        }

        /// <summary>
        /// Checks if the provided type is <see cref="Nullable{T}" />.
        /// </summary>
        /// <param name="type">Type value to check.</param>
        /// <returns>
        /// <see langword="true" /> if type is kind of <see cref="Nullable{T}" />,
        /// <see langword="false" /> otherwise.
        /// </returns>
        public static bool IsNullableType(this Type type)
        {
            return type != null &&
                   type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Extracts non-nullable type from provided type.
        /// </summary>
        /// <param name="type">Type to unwrap.</param>
        /// <returns>
        /// Internal type if the type is kind of <see cref="Nullable{T}" />,
        /// original <paramref name="type" /> parameter value otherwise.
        /// </returns>
        public static Type GetNonNullableType(this Type type)
        {
            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }

            return type;
        }
    }
}
