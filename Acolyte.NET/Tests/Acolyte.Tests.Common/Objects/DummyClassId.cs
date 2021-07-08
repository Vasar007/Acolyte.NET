using System;
using System.Collections.Generic;
using Acolyte.Assertions;

namespace Acolyte.Tests.Objects
{
    // Implicitly implements "IEquatable<DummyIdClass>" because it is record.
    public sealed record DummyClassId : IComparable<DummyClassId>
    {
        public static IReadOnlyList<DummyClassId> DefaultList { get; } = new List<DummyClassId>
        {
            new DummyClassId(Guid.Parse("CF6A7E69-45E7-4999-8007-55FF2862AE01"), 1),
            new DummyClassId(Guid.Parse("CF6A7E69-45E7-4999-8007-55FF2862AE02"), 2),
            new DummyClassId(Guid.Parse("CF6A7E69-45E7-4999-8007-55FF2862AE03"), 3),
            new DummyClassId(Guid.Parse("CF6A7E69-45E7-4999-8007-55FF2862AE04"), 4),
            new DummyClassId(Guid.Parse("CF6A7E69-45E7-4999-8007-55FF2862AE05"), 5)
        }.AsReadOnly();

        public static DummyClassId Item { get; } =
            new(Guid.Parse("CF6A7E69-45E7-4999-8007-55FF2862AE42"), 42);

        public Guid Id { get; init; }
        public int Value { get; init; }


        public DummyClassId(
            Guid id,
            int value)
        {
            Id = id.ThrowIfEmpty(nameof(id));
            Value = value;
        }

        #region IComparable<DummyIdClass> Implementation

        public int CompareTo(DummyClassId other)
        {
            int valueComparisonResult = Value.CompareTo(other.Value);
            if (valueComparisonResult != 0)
            {
                return valueComparisonResult;
            }

            return Id.CompareTo(other.Id);
        }

        #endregion

        public static bool operator <(DummyClassId left, DummyClassId right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        public static bool operator <=(DummyClassId left, DummyClassId right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator >(DummyClassId left, DummyClassId right)
        {
            return left is not null && left.CompareTo(right) > 0;
        }

        public static bool operator >=(DummyClassId left, DummyClassId right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
    }

    public sealed class DummyClassIdValueOnlyEqualityComparer : IEqualityComparer<DummyClassId>
    {
        public static DummyClassIdValueOnlyEqualityComparer Instance { get; } = new();


        public DummyClassIdValueOnlyEqualityComparer()
        {
        }

        #region IEqualityComparer<DummyIdClass> Implementation

        public bool Equals(DummyClassId x, DummyClassId y)
        {
            if (x is null) return y is null;
            if (y is null) return false;

            return x.Value.Equals(y.Value);
        }

        public int GetHashCode(DummyClassId obj)
        {
            return HashCode.Combine(obj.Value);
        }

        #endregion
    }
}
