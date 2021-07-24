using System;
using System.Globalization;
using Acolyte.Assertions;
using Acolyte.Linq;

namespace Acolyte.Common
{
    /// <summary>
    /// Bounds some value with string reason (can be also <see cref="FormatString" />).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// The key difference between <see cref="Reasonable{T}" /> and
    /// <see cref="Result{TOk, TError}" /> is usage:
    /// <see cref="Reasonable{T}" /> can be use to bound any object in any case (failed or
    /// successful result) but <see cref="Result{TOk, TError}" /> can be used only with final
    /// result status. In short, <see cref="Reasonable{T}" /> is
    /// Result&lt;<typeparamref name="T" /> , <see cref="string" />&gt; without boolean flag
    /// IsSuccess. 
    /// </remarks>
    public readonly struct Reasonable<T> : IEquatable<Reasonable<T>>, IFormattable
    {
        /// <summary>
        /// Field that hold value.
        /// </summary>
        public T? Value { get; }

        public T CheckedValue => Value is null
            ? throw new InvalidOperationException($"{nameof(Value)} property was null.")
            : Value;

        public object Reason { get; }

        public string ReasonString => Reason.ToString();

        public string FormattedString => ToString();


        public Reasonable(
            T? value,
            object reason)
        {
            Value = value;
            Reason = reason.ThrowIfNull(nameof(reason));
        }

        #region Object Overridden Methods

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is Reasonable<T> other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return System.HashCode.Combine(Value, Reason);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return ToString(format: null, formatProvider: CultureInfo.CurrentCulture);
        }

        #endregion

        #region IEquatable<Reasonable<T>> Implementation

        /// <inheritdoc />
        public bool Equals(Reasonable<T> other)
        {
            if (!Reason.Equals(other.Reason)) return false;

            return Value is null
                ? other.Value is null
                : Value.Equals(other.Value);
        }

        #endregion

        public static implicit operator T?(Reasonable<T> value)
        {
            return value.Value;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Reasonable{T}" /> are equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if values are memberwise equals; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public static bool operator ==(Reasonable<T> left, Reasonable<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Reasonable{T}" /> are not
        /// equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if values are not memberwise equals; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public static bool operator !=(Reasonable<T> left, Reasonable<T> right)
        {
            return !(left == right);
        }

        #region IFormattable Implementation

        /// <inheritdoc />
        public string ToString(string? format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case null: // {0}
                {
                    return string.Format(
                        formatProvider, "{0} ({1})", Value, Reason
                    );
                }

                default: // {0:xxxx}
                {
                    return string.Format(
                        formatProvider, "{0} ({1}) - Invalid format: {2}", Value, Reason, format
                    );
                }
            }
        }

        #endregion
    }

    public static class Reasonable
    {
        public static Reasonable<T> Ok<T>(T? value)
        {
            return Wrap(value, "Ok");
        }

        public static Reasonable<T> Wrap<T>(T? value, string reasonFormat, params object[] args)
        {
            if (args.IsNullOrEmpty())
            {
                return new Reasonable<T>(value, reasonFormat);
            }
            else
            {
                var reason = new FormatString(reasonFormat, args);
                return new Reasonable<T>(value, reason);
            }
        }

        public static Reasonable<T1> Wrap<T1, T2>(T1 value, Reasonable<T2> reasonable)
        {
            return new Reasonable<T1>(value, reasonable.Reason);
        }

        public static Reasonable<bool> And(this Reasonable<bool> self, Reasonable<bool> other)
        {
            return self.Value
                ? other
                : self;
        }

        public static Reasonable<bool> And(this Reasonable<bool> self, Func<Reasonable<bool>> other)
        {
            return self.Value
                ? other()
                : self;
        }

        public static Reasonable<bool> Or(this Reasonable<bool> self, Reasonable<bool> other)
        {
            return self.Value
                ? self
                : other;
        }

        public static Reasonable<bool> Or(this Reasonable<bool> self, Func<Reasonable<bool>> other)
        {
            return self.Value
                ? self
                : other();
        }

        public static Reasonable<bool> Not(Reasonable<bool> reasonable)
        {
            return new Reasonable<bool>(!reasonable.Value, reasonable.Reason);
        }
    }
}
