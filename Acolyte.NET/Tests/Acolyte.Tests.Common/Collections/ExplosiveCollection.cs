using System.Collections;
using System.Collections.Generic;
using Acolyte.Assertions;
using Acolyte.Common;

namespace Acolyte.Tests.Collections
{
    public sealed class ExplosiveCollection<T> : IEnumerable<T?>, IEnumerable
    {
        private readonly IEnumerable<T> _originalCollection;

        private readonly int _explosiveIndex;

        private readonly CounterInt32 _visitedItemsNumber;
        public int VisitedItemsNumber => _visitedItemsNumber.Value;


        public ExplosiveCollection(
            IEnumerable<T> originalCollection,
            int explosiveIndex)
        {
            _originalCollection = originalCollection.ThrowIfNull(nameof(originalCollection));
            _explosiveIndex = explosiveIndex.ThrowIfValueIsOutOfRange(
                nameof(explosiveIndex), Constants.NotFoundIndex, int.MaxValue
            );

            _visitedItemsNumber = new CounterInt32();
        }

        #region IEnumerable<T?> Implementation

        public IEnumerator<T?> GetEnumerator()
        {
            return ExplosiveEnumerator.Create(
                _originalCollection.GetEnumerator(),
                _explosiveIndex,
                _visitedItemsNumber.Reset()
            );
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    public static class ExplosiveCollection
    {
        public static ExplosiveCollection<T> Create<T>(
            IEnumerable<T> originalCollection,
            int explosiveIndex)
        {
            return new ExplosiveCollection<T>(
                originalCollection: originalCollection,
                explosiveIndex: explosiveIndex
            );
        }

        public static ExplosiveCollection<T> CreateNotExplosive<T>(
           IEnumerable<T> originalCollection)
        {
            return new ExplosiveCollection<T>(
                originalCollection: originalCollection,
                explosiveIndex: Constants.NotFoundIndex
            );
        }
    }
}
