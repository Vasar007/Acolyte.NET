using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Collections
{
    public sealed class InverseComparer<T> : IComparer<T>
    {
        private readonly IComparer<T> _comparer;


        public InverseComparer(IComparer<T> comparer)
        {
            _comparer = comparer.ThrowIfNull(nameof(comparer));
        }

        #region IComparer<T> Implementation

        public int Compare(T obj1, T obj2)
        {
            int comparsion = _comparer.Compare(obj1, obj2);
            return -comparsion;
        }

        #endregion
    }
}
