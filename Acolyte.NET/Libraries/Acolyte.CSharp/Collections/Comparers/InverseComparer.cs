using System.Collections.Generic;

namespace Acolyte.Collections.Comparers
{
    /// <summary>
    /// Allows to invert every object of <see cref="IComparer{T}" /> type.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    public sealed class InverseComparer<T> : IComparer<T>
    {
        private readonly IComparer<T> _comparer;


        public InverseComparer(
            IComparer<T>? comparer)
        {
            _comparer = comparer ?? Comparer<T>.Default;
        }

        #region IComparer<T> Implementation

        public int Compare(T obj1, T obj2)
        {
            int comparison = _comparer.Compare(obj1, obj2);
            return -comparison;
        }

        #endregion
    }
}
