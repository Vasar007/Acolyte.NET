using System.Collections;
using System.Collections.Generic;
using Acolyte.Assertions;
using Acolyte.Common;
using Acolyte.Threading;

namespace Acolyte.Tests.Collections
{
    public sealed class ExplosiveEnumerable<T> : IEnumerable<T?>, IEnumerable
    {
        private readonly IEnumerable<T> _originalEnumerable;

        private readonly int _explosiveIndex;

        private readonly CounterInt32 _visitedItemsNumber;
        public int VisitedItemsNumber => _visitedItemsNumber.Value;


        public ExplosiveEnumerable(
            IEnumerable<T> originalEnumerable,
            int explosiveIndex)
        {
            _originalEnumerable = originalEnumerable.ThrowIfNull(nameof(originalEnumerable));
            _explosiveIndex = explosiveIndex.ThrowIfValueIsOutOfRange(
                nameof(explosiveIndex), Constants.NotFoundIndex, int.MaxValue
            );

            _visitedItemsNumber = new CounterInt32();
        }

        #region IEnumerable<T?> Implementation

        public IEnumerator<T?> GetEnumerator()
        {
            return ExplosiveEnumerator.Create(
                _originalEnumerable.GetEnumerator(),
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

    public static class ExplosiveEnumerable
    {
        public static ExplosiveEnumerable<T> Create<T>(
            IEnumerable<T> originalEnumerable,
            int explosiveIndex)
        {
            return new ExplosiveEnumerable<T>(
                originalEnumerable: originalEnumerable,
                explosiveIndex: explosiveIndex
            );
        }

        public static ExplosiveEnumerable<T> CreateNotExplosive<T>(
           IEnumerable<T> originalEnumerable)
        {
            return new ExplosiveEnumerable<T>(
                originalEnumerable: originalEnumerable,
                explosiveIndex: Constants.NotFoundIndex
            );
        }
    }
}
