using System;
using System.Collections.Generic;
using System.Text;

namespace Acolyte.Tests.Objects
{
    public readonly struct DummyStruct : IEquatable<DummyStruct>, IComparable<DummyStruct>
    {
        public static IReadOnlyList<DummyStruct> DefaultList = new List<DummyStruct>
        {
            new DummyStruct(1),
            new DummyStruct(2),
            new DummyStruct(3),
            new DummyStruct(4),
            new DummyStruct(5)
        }.AsReadOnly();

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

        #region Object Overridden Methods

        /// <inheritdoc />
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("DummyStruct");
            stringBuilder.Append(" { ");
            if (PrintMembers(stringBuilder))
            {
                stringBuilder.Append(" ");
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        private bool PrintMembers(StringBuilder builder)
        {
            builder.Append("Value");
            builder.Append(" = ");
            builder.Append(Value.ToString());
            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj is not DummyStruct other) return false;

            return Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        #endregion

        #region IEquatable<DummyStruct> Implementation

        /// <inheritdoc />
        public bool Equals(DummyStruct other)
        {
            return Value.Equals(other.Value);
        }

        #endregion

        #region IComparable<DummyStruct> Implementation

        public int CompareTo(DummyStruct other)
        {
            return Value.CompareTo(other.Value);
        }

        #endregion

        /// <summary>
        /// Determines whether two specified instances of <see cref="DummyStruct" /> are
        /// equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if values are memberwise equals,
        /// <see langword="false" /> otherwise.
        /// </returns>
        public static bool operator ==(DummyStruct left, DummyStruct right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="DummyStruct" /> are not equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if values are not memberwise equals, <see langword="false" />
        /// otherwise.
        /// </returns>
        public static bool operator !=(DummyStruct left, DummyStruct right)
        {
            return !(left == right);
        }

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
