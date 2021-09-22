using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Acolyte.Assertions;

namespace Acolyte.Reflection
{
    public static class ReflectionExtensions
    {
        private const BindingFlags PublicInstanceFlags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.FlattenHierarchy;

        /// <inheritdoc cref="EnumeratePublicInstanceFields(object)" />
        [Obsolete("Use \"Acolyte.Reflection.ReflectionExtensions.EnumeratePublicInstanceFields\" instead. This method will be remove in next major version.", error: false)]
        public static IEnumerable<object?> GetPublicInstanceFields(
            this object objectToInspect)
        {
            return objectToInspect.EnumeratePublicInstanceFields();
        }

        public static IEnumerable<object> EnumeratePublicInstanceFields(
            this object objectToInspect)
        {
            objectToInspect.ThrowIfNull(nameof(objectToInspect));

            return objectToInspect
                .GetType()
                .GetFields(PublicInstanceFlags)
                .Select(field => field.GetValue(objectToInspect));
        }

        public static IEnumerable<object> EnumeratePublicInstanceProperties(
            this object objectToInspect)
        {
            objectToInspect.ThrowIfNull(nameof(objectToInspect));

            return objectToInspect
                .GetType()
                .GetProperties(PublicInstanceFlags)
                .Select(field => field.GetValue(objectToInspect));
        }
    }
}
