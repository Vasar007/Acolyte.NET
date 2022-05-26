using System;
using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;

namespace Acolyte.Common
{
    public sealed class FormatString : IEquatable<FormatString>
    {
        private readonly Lazy<string> _lazyValue;

        public string Value => _lazyValue.Value;


        public FormatString(
            string format,
            IEnumerable<object> args)
        {
            format.ThrowIfNull(nameof(format));
            args.ThrowIfNull(nameof(args));

            _lazyValue = new Lazy<string>(() => FormatInternal(format, args));
        }

        public FormatString(
            string format,
            params object[] args)
            : this(format, args.ThrowIfNull(nameof(args)).AsEnumerable())
        {
        }

        #region Object Overridden Methods

        /// <inheritdoc />
        public override string ToString()
        {
            return Value;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return System.HashCode.Combine(Value);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return Equals(obj as FormatString);
        }

        #endregion

        #region IEquatable<FormatString> Implementation

        /// <inheritdoc />
        public bool Equals(FormatString? other)
        {
            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            return Value.Equals(other.Value);
        }

        #endregion

        /// <summary>
        /// Determines whether two specified instances of <see cref="FormatString" /> are equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if values are memberwise equals; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public static bool operator ==(FormatString? left, FormatString? right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="FormatString" /> are not equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if values are not memberwise equals; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public static bool operator !=(FormatString? left, FormatString? right)
        {
            return !(left == right);
        }

        private string FormatInternal(string format, IEnumerable<object> args)
        {
            return string.Format(format, args);
        }
    }
}
