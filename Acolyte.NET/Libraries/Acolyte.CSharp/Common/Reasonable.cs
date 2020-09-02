﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Acolyte.Assertions;
using Acolyte.Collections;

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
    /// Result&lt;<see cref="T" /> , <see cref="string" />&gt; without boolean flag IsSuccess.
    /// </remarks>
    public readonly struct Reasonable<T> : IEquatable<Reasonable<T>>
    {
        /// <summary>
        /// Field that hold value.
        /// </summary>
        [MaybeNull, AllowNull]
        public T Value { get; }

        public T CheckedValue => Value is null
            ? throw new InvalidOperationException($"{nameof(Value)} property was null.")
            : Value;

        public object Reason { get; }

        public string ReasonString => Reason.ToString();

        public string FormattedString => ToString();


        public Reasonable(
            [AllowNull] T value,
            object reason)
        {
            Value = value;
            Reason = reason.ThrowIfNull(nameof(reason));
        }

        #region Object Overridden Methods

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (!(obj is Reasonable<T> other)) return false;

            return Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            // Method "HashCode.Combine" acceps parameters with null values.
#pragma warning disable CS8604 // Possible null reference argument.
            return HashCode.Combine(Value, Reason);
#pragma warning restore CS8604 // Possible null reference argument.
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

        [return: MaybeNull]
        public static implicit operator T(Reasonable<T> value)
        {
            return value.Value;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Reasonable{T}" /> are equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns><c>true</c> if values are memberwise equals, <c>false</c> otherwise.</returns>
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
        /// <c>true</c> if values are not memberwise equals, <c>false</c> otherwise.
        /// </returns>
        public static bool operator !=(Reasonable<T> left, Reasonable<T> right)
        {
            return !(left == right);
        }

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
    }

    public static class Reasonable
    {
        public static Reasonable<T> Ok<T>(T value)
        {
            return Wrap(value, "Ok");
        }

        public static Reasonable<T> Wrap<T>(T value, string reasonFormat, params object[] args)
        {
            if (args.IsNullOrEmpty())
            {
                return new Reasonable<T>(value, reasonFormat);
            }
            else
            {
                FormatString reason = new FormatString(reasonFormat, args);
                return new Reasonable<T>(value, reason);
            }
        }

        public static Reasonable<T1> Wrap<T1, T2>(T1 value, Reasonable<T2> reasonable)
        {
            return new Reasonable<T1>(value, reasonable.Reason);
        }

        public static Reasonable<bool> Or(this Reasonable<bool> self, Reasonable<bool> other)
        {
            return self.Value
                ? self
                : other;
        }
    }
}