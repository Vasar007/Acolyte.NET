using System.Collections.Generic;
using Acolyte.Tests.Mocked;

namespace Acolyte.Tests.Linq.ToReadOnlyCollection
{
    public sealed partial class ToReadOnlyDictionaryTests
    {
        public ToReadOnlyDictionaryTests()
        {
        }

        #region Private Methods

        private static void VerifyMethodsCallsForToReadOnlyDictionary<T>(
            MockEqualityComparer<T> comparer, IReadOnlyList<T> collection)
        {
            comparer.VerifyEqualsNoCalls();
            comparer.VerifyGetHashCodeCallsTwiceForEach(collection);
        }

        #endregion
    }
}
