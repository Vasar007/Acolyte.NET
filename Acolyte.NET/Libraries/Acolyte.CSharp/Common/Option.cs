using System;
using System.Diagnostics.CodeAnalysis;

namespace Acolyte.Common
{
    /// <summary>
    /// Represents functional option type.
    /// </summary>
    /// <typeparam name="T">The type of underlying item value.</typeparam>
    public readonly struct Option<T> : IEquatable<Option<T>>
    {
        /// <summary>
        /// Field that hold value object reference.
        /// </summary>
        [AllowNull]
        private readonly T _value;

        /// <summary>
        /// Gets a value indicating whether the current <see cref="Option{T}" /> object has a valid
        /// value of its underlying type.
        /// </summary>
        public bool HasValue { get; }

        /// <summary>
        /// Gets the value of the current <see cref="Option{T}" /> object if it has been assigned a
        /// valid underlying value.
        /// </summary>
        /// <returns>
        /// The value of the current object if the <see cref="HasValue" /> property is <c>true</c>.
        /// An exception is thrown if the <see cref="HasValue" /> property is <c>false</c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <see cref="HasValue" /> property is <c>false</c>.
        /// </exception>
        [MaybeNull]
        public T Value => HasValue
            ? _value
            : throw new InvalidOperationException($"{nameof(Value)} is not active.");

        /// <summary>
        /// Initializes a new instance of the <see cref="Option{T}" /> structure to the specified
        /// value.
        /// </summary>
        /// <param name="value">A reference type.</param>
        public Option([AllowNull] T value)
            : this()
        {
            _value = value;
            HasValue = value is null;
        }

        /// <summary>
        /// Implicitly converts object of type <typeparamref name="T" /> to
        /// <see cref="Option{T}" />.
        /// </summary>
        /// <param name="value">A reference type to convert.</param>
        public static implicit operator Option<T>([AllowNull] T value)
        {
            return new Option<T>(value);
        }

        /// <summary>
        /// Explicitly converts <see cref="Option{T}" /> to object of type
        /// <typeparamref name="T" />.
        /// </summary>
        /// <param name="option">An option value to convert.</param>
        /// <exception cref="InvalidOperationException">
        /// <see cref="HasValue" /> property is <c>false</c>.
        /// </exception>
        [return: MaybeNull]
        public static explicit operator T(Option<T> option)
        {
            return option.Value;
        }

        /// <summary>
        /// Implicitly converts object of type <see cref="Option{T}" /> to <see cref="bool" />.
        /// </summary>
        /// <param name="option">An option value to convert.</param>
        public static implicit operator bool(Option<T> option)
        {
            return option.HasValue;
        }

        #region Object Overridden Methods

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (!(obj is Option<T> other)) return false;

            return Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return !HasValue || _value is null
                ? 0
                : _value.GetHashCode();
        }

        #endregion

        #region IEquatable<Option<T>> Implementation

        /// <inheritdoc />
        public bool Equals(Option<T> other)
        {
            if (!HasValue.Equals(other.HasValue)) return false;

            return HasValue && !(_value is null)
                ? _value.Equals(other._value)
                : other._value is null;
        }

        #endregion

        /// <summary>
        /// Determines whether two specified instances of <see cref="Option{T}" /> are equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns><c>true</c> if values are memberwise equals, <c>false</c> otherwise.</returns>
        public static bool operator ==(Option<T> left, Option<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Option{T}" /> are not equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <c>true</c> if values are not memberwise equals, <c>false</c> otherwise.
        /// </returns>
        public static bool operator !=(Option<T> left, Option<T> right)
        {
            return !(left == right);
        }

        public static implicit operator Option<T>(NoneOption none)
        {
            return new Option<T>();
        }

        public TOut Match<TOut>(Func<T, TOut> some, Func<TOut> none)
        {
            return HasValue
                ? some(_value)
                : none();
        }

        public void Match(Action<T> some, Action none)
        {
            if (HasValue)
            {
                some(_value);
            }
            else
            {
                none();
            }
        }

        public Option<TOut> Select<TOut>(Func<T, TOut> map)
        {
            return HasValue
                ? new Option<TOut>(map(_value))
                : new Option<TOut>();
        }

        public Option<TOut> Bind<TOut>(Func<T, Option<TOut>> bind)
        {
            return HasValue
                ? bind(_value)
                : new Option<TOut>();
        }
    }

    public readonly struct NoneOption
    {
    }

    public static class Option
    {
        public static Option<T> Some<T>(T value)
        {
            return new Option<T>(value);
        }

        public static NoneOption None { get; } = new NoneOption();
    }
}
