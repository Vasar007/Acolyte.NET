using System;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Collections.Comparers
{
    public sealed class LambdaEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _lambdaComparer;
        private readonly Func<T, int> _lambdaHash;


        public LambdaEqualityComparer(
            Func<T, T, bool> lambdaComparer,
            Func<T, int> lambdaHash)
        {
            _lambdaComparer = lambdaComparer.ThrowIfNull(nameof(lambdaComparer));
            _lambdaHash = lambdaHash.ThrowIfNull(nameof(lambdaHash));
        }

        public static LambdaEqualityComparer<T> Create(
            Func<T, T, bool> lambdaComparer)
        {
            return new LambdaEqualityComparer<T>(
                lambdaComparer: lambdaComparer,
                lambdaHash: _ => 0
            );
        }

        #region Implementation of IEqualityComparer<T>

        public bool Equals(T x, T y)
        {
            return _lambdaComparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _lambdaHash(obj);
        }

        #endregion
    }
}
