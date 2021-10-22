using System;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Collections.Comparers
{
    public sealed class LambdaComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, int> _lambdaComparer;


        public LambdaComparer(
            Func<T, T, int> lambdaComparer)
        {
            _lambdaComparer = lambdaComparer.ThrowIfNull(nameof(lambdaComparer));
        }

        #region Implementation of IComparer<T>

        public int Compare(T x, T y)
        {
            return _lambdaComparer(x, y);
        }

        #endregion
    }
}
