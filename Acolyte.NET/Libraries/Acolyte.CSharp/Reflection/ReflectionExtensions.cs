using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Acolyte.Assertions;

namespace Acolyte.Reflection
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<object?> GetPublicInstanceFields(
            this object objectToInspect)
        {
            objectToInspect.ThrowIfNull(nameof(objectToInspect));

            return objectToInspect
                .GetType()
                .GetFields(BindingFlags.Instance |
                           BindingFlags.Public |
                           BindingFlags.FlattenHierarchy)
                // "Cast" to "object?" because fields can be nullable.
                .Select(field => field.GetValue(objectToInspect));
        }
    }
}
