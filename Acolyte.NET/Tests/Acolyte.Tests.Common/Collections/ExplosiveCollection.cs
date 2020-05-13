using System;
using System.Collections;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Tests.Collections
{
    public sealed class ExplosiveCollection<T> : IEnumerable<T>, IEnumerable
    {
        private readonly IEnumerable<T> _originalCollection;

        private readonly int _explosiveIndex;


        public ExplosiveCollection(
            IEnumerable<T> originalCollection,
            int explosiveIndex)
        {
            _originalCollection = originalCollection.ThrowIfNull(nameof(originalCollection));
            _explosiveIndex =
                explosiveIndex.ThrowIfValueIsOutOfRange(nameof(explosiveIndex), 0, int.MaxValue);
        }

        #region IEnumerable<T> Implementation

        public IEnumerator<T> GetEnumerator()
        {
            return ExplosiveEnumerator.Create(
                _originalCollection.GetEnumerator(),
                _explosiveIndex
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
    }
}
