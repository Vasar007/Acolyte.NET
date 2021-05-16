using System;
using Acolyte.Assertions;

namespace Acolyte.Common
{
    /// <summary>
    /// Defines extension methods for <see cref="Type" /> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks that <paramref name="potentialDescendant" /> is same type as
        /// <paramref name="potentialBase" /> or is the it's subclass.
        /// </summary>
        /// <param name="potentialDescendant">Potential descendant type to check.</param>
        /// <param name="potentialBase">Potential base type to compare.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="potentialDescendant" /> is same type as
        /// <paramref name="potentialBase" /> or is the it subclass; otherwise,
        /// <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="potentialDescendant" /> is <see langword="null" /> -or-
        /// <paramref name="potentialBase" /> is <see langword="null" />.
        /// </exception>
        public static bool IsSameOrSubclass(this Type potentialDescendant, Type potentialBase)
        {
            // Null check for "potentialBase" parameter is provided by "Type.IsSubclassOf" method.
            potentialDescendant.ThrowIfNull(nameof(potentialDescendant));

            return potentialDescendant.IsSubclassOf(potentialBase) ||
                   potentialDescendant.Equals(potentialBase);
        }

        /// <summary>
        /// Checks if the provided types have the same generic type definitions.
        /// </summary>
        /// <param name="type">Type value to check.</param>
        /// <param name="typeToCompare">Type value to compare with <paramref name="type"/>.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="type"/> has the same generic type definition
        /// as of <paramref name="typeToCompare"/>; otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type" /> is <see langword="null" />. -or-
        /// <paramref name="typeToCompare" /> is <see langword="null" />.
        /// </exception>
        public static bool HasSameGenericTypeDefinition(this Type type, Type typeToCompare)
        {
            type.ThrowIfNull(nameof(type));
            typeToCompare.ThrowIfNull(nameof(typeToCompare));

            return type.IsGenericType &&
                   typeToCompare.IsGenericType &&
                   type.GetGenericTypeDefinition().Equals(typeToCompare.GetGenericTypeDefinition());
        }

        /// <summary>
        /// Checks if the provided <paramref name="type"/> is <see cref="Nullable{T}" />.
        /// </summary>
        /// <param name="type">Type value to check.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="type"/> is kind of
        /// <see cref="Nullable{T}" />; otherwise, <see langword="false" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type" /> is <see langword="null" />.
        /// </exception>
        public static bool IsNullableType(this Type type)
        {
            type.ThrowIfNull(nameof(type));

            return type.HasSameGenericTypeDefinition(typeof(Nullable<>));
        }

        /// <summary>
        /// Extracts non-nullable type from provided type.
        /// </summary>
        /// <param name="type">Type to unwrap.</param>
        /// <returns>
        /// Internal type if the type is kind of <see cref="Nullable{T}" />; otherwise,
        /// original <paramref name="type" /> parameter value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type" /> is <see langword="null" />.
        /// </exception>
        public static Type GetNonNullableType(this Type type)
        {
            type.ThrowIfNull(nameof(type));

            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }

            return type;
        }
    }
}
