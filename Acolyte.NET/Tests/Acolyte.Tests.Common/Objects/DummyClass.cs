using System;
using System.Collections.Generic;

namespace Acolyte.Tests.Objects
{
    // Implicitly implements "IEquatable<DummyClass>" because it is record.
    public sealed record DummyClass(int Value) : IComparable<DummyClass>
    {
        public static IReadOnlyList<DummyClass> DefaultList = new List<DummyClass>
        {
            new DummyClass(1),
            new DummyClass(2),
            new DummyClass(3),
            new DummyClass(4),
            new DummyClass(5)
        }.AsReadOnly();

        #region IComparable<DummyClass> Implementation

        public int CompareTo(DummyClass other)
        {
            return Value.CompareTo(other.Value);
        }

        #endregion

        public static bool operator <(DummyClass left, DummyClass right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        public static bool operator <=(DummyClass left, DummyClass right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator >(DummyClass left, DummyClass right)
        {
            return left is not null && left.CompareTo(right) > 0;
        }

        public static bool operator >=(DummyClass left, DummyClass right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
    }
}
