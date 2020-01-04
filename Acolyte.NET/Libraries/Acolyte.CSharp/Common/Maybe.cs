using System;
using System.Diagnostics.CodeAnalysis;

namespace Acolyte.Common
{
    /// <summary>
    /// Represents similar interface to nullable struct but for reference types.
    /// </summary>
    /// <typeparam name="T">The type of underlying item value.</typeparam>
    public readonly struct Maybe<T> : IEquatable<Maybe<T>>
        where T : class
    {
        /// <summary>
        /// Field that hold value object reference.
        /// </summary>
        [MaybeNull]
        private readonly T _value;

        /// <summary>
        /// Gets a value indicating whether the current <see cref="Maybe{T}" /> object has a valid
        /// value of its underlying type.
        /// </summary>
        public bool HasValue { get; }

        /// <summary>
        /// Gets the value of the current <see cref="Maybe{T}" /> object if it has been assigned a
        /// valid underlying value.
        /// </summary>
        /// <returns>
        /// The value of the current object if the <see cref="HasValue" /> property is <c>true</c>.
        /// An exception is thrown if the <see cref="HasValue" /> property is <c>false</c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <see cref="HasValue" /> property is <c>false</c>.
        /// </exception>
        [NotNull]
        public T Value => HasValue
            ? _value
            : throw new InvalidOperationException($"{nameof(Value)} is not active.");

        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{T}" /> structure to the specified
        /// value.
        /// </summary>
        /// <param name="value">A reference type.</param>
        public Maybe([AllowNull] T value)
            : this()
        {
            _value = value;
            HasValue = value is null;
        }

        /// <summary>
        /// Implicitly converts object of type <typeparamref name="T" /> to <see cref="Maybe{T}" />.
        /// </summary>
        /// <param name="value">A reference type to convert.</param>
        public static implicit operator Maybe<T>([AllowNull] T value)
        {
            return new Maybe<T>(value);
        }

        /// <summary>
        /// Explicitly converts <see cref="Maybe{T}" /> to object of type <typeparamref name="T" />.
        /// </summary>
        /// <param name="value">A value to convert.</param>
        /// <exception cref="InvalidOperationException">
        /// <see cref="HasValue" /> property is <c>false</c>.
        /// </exception>
        [return: NotNull]
        public static explicit operator T(Maybe<T> value)
        {
            return value.Value;
        }

        #region Object Overridden Methods

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (!(obj is Maybe<T> other)) return false;

            return IsEqual(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return !HasValue ? 0 : _value.GetHashCode();
        }

        #endregion

        #region IEquatable<Maybe<T>> Implementation

        /// <inheritdoc />
        public bool Equals(Maybe<T> other)
        {
            return IsEqual(other);
        }

        #endregion

        /// <summary>
        /// Determines whether two specified instances of <see cref="Maybe{T}" /> are equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns><c>true</c> if values are memberwise equals, <c>false</c> otherwise.</returns>
        public static bool operator ==(Maybe<T> left, Maybe<T> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Maybe{T}" /> are not equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <c>true</c> if values are not memberwise equals, <c>false</c> otherwise.
        /// </returns>
        public static bool operator !=(Maybe<T> left, Maybe<T> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether specified instance of <see cref="Maybe{T}" /> is equal to caller
        /// object.
        /// </summary>
        /// <param name="other">Other object to compare.</param>
        /// <returns><c>true</c> if values are memberwise equals, <c>false</c> otherwise.</returns>
        private bool IsEqual(Maybe<T> other)
        {
            return HasValue.Equals(other.HasValue) &&
                   HasValue
                       ? _value.Equals(other.Value)
                       : _value is null && other._value is null;
        }
    }
}
