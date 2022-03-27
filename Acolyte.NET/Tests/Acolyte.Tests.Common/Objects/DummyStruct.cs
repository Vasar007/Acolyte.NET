using System;
using System.Collections.Generic;

namespace Acolyte.Tests.Objects
{
    // Implicitly implements "IEquatable<DummyStruct>" because it is record.
    public readonly record struct DummyStruct : IComparable<DummyStruct>
    {
        public static IReadOnlyList<DummyStruct> DefaultList { get; } = new List<DummyStruct>
        {
            new DummyStruct(1),
            new DummyStruct(2),
            new DummyStruct(3),
            new DummyStruct(4),
            new DummyStruct(5)
        }.AsReadOnly();

        public static DummyStruct Item { get; } = new(42);

        public int Value { get; init; }

        public DummyStruct(
            int value)
        {
            Value = value;
        }

        public void Deconstruct(out int value)
        {
            value = Value;
        }

        #region IComparable<DummyStruct> Implementation

        public int CompareTo(DummyStruct other)
        {
            return Value.CompareTo(other.Value);
        }

        #endregion

        public static bool operator <(DummyStruct left, DummyStruct right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(DummyStruct left, DummyStruct right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(DummyStruct left, DummyStruct right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(DummyStruct left, DummyStruct right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
