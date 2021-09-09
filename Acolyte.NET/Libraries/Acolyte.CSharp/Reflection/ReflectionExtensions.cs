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

            const BindingFlags bindingAttr =
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.FlattenHierarchy;

            return objectToInspect
                .GetType()
                .GetFields(bindingAttr)
                .Select(field => field.GetValue(objectToInspect));
        }
    }
}
