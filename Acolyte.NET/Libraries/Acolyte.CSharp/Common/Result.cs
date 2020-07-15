using System;
using System.Diagnostics.CodeAnalysis;

namespace Acolyte.Common
{
    public readonly struct Result<TOk, TError> : IEquatable<Result<TOk, TError>>
    {
        [AllowNull]
        private readonly TOk _ok;

        [AllowNull]
        private readonly TError _error;

        public bool IsSuccess { get; }

        [MaybeNull]
        public TOk Ok => IsSuccess
            ? _ok
            : throw new InvalidOperationException($"{nameof(Ok)} property was not active.");

        [MaybeNull]
        public TError Error => !IsSuccess
            ? _error
            : throw new InvalidOperationException($"{nameof(Error)} property was not active.");


        private Result([AllowNull] TOk ok, [AllowNull] TError error, bool isSuccess)
        {
            _ok = ok;
            _error = error;
            IsSuccess = isSuccess;
        }

        public Result([AllowNull] TOk ok)
            : this(ok, default, true)
        {
        }

        public Result([AllowNull] TError error)
            : this(default, error, false)
        {
        }

        #region Object Overridden Methods

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (!(obj is Result<TOk, TError> other)) return false;

            return Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (IsSuccess)
            {
                return !(_ok is null)
                    ? _ok.GetHashCode()
                    : 0;
            }

            return !(_error is null)
                ? _error.GetHashCode()
                : 0;
        }

        #endregion

        #region IEquatable<Result<TOk, TError>> Implementation

        /// <inheritdoc />
        public bool Equals(Result<TOk, TError> other)
        {
            if (!IsSuccess.Equals(other.IsSuccess)) return false;

            if (IsSuccess)
            {
                return !(_ok is null)
                    ? _ok.Equals(other._ok)
                    : other._ok is null;
            }

            return !(_error is null)
                ? _error.Equals(other._error)
                : other._error is null;
        }

        #endregion

        public static implicit operator bool(Result<TOk, TError> result)
        {
            return result.IsSuccess;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Result{TValue, TError}" /> are
        /// equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns><c>true</c> if values are memberwise equals, <c>false</c> otherwise.</returns>
        public static bool operator ==(Result<TOk, TError> left, Result<TOk, TError> right)
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
        public static bool operator !=(Result<TOk, TError> left, Result<TOk, TError> right)
        {
            return !(left == right);
        }

        public static implicit operator Result<TOk, TError>(DelayedValue<TOk> ok)
        {
            return new Result<TOk, TError>(ok.Value);
        }

        public static implicit operator Result<TOk, TError>(DelayedError<TError> error)
        {
            return new Result<TOk, TError>(error.Value);
        }

        /// <summary>
        /// Retrieves the value of the current <see cref="Result{TValue, TError}" /> object, or the
        /// specified default value.
        /// </summary>
        /// <param name="defaultValue">
        /// A value to return if the <see cref="IsSuccess" /> property is <c>true</c>.
        /// </param>
        /// <returns>
        /// The value of the <see cref="Result" /> property if the <see cref="IsSuccess" /> property
        /// is <c>false</c>; otherwise, the <paramref name="defaultValue" /> parameter.
        ///</returns>
        [return: MaybeNull]
        public TOk GetValueOrDefault([AllowNull] TOk defaultValue = default)
        {
            return IsSuccess
                ? _ok
                : defaultValue;
        }

        /// <summary>
        /// Retrieves the exception value of the current <see cref="ResultOrException{T}" /> object,
        /// or the specified default exception value.
        /// </summary>
        /// <param name="defaultException">
        /// An exception value to return if the <see cref="IsSuccess" /> property is <c>false</c> and
        /// exception value is not <c>null</c>.
        /// </param>
        /// <returns>
        /// The exception value of the <see cref="Exception" /> property if the
        /// <see cref="IsSuccess" /> property is <c>true</c>; otherwise, the
        /// <paramref name="defaultException" /> parameter.
        ///</returns>
        [return: MaybeNull]
        public TError GetExceptionOrDefault([AllowNull] TError defaultException = default)
        {
            return !IsSuccess
                ? _error
                : defaultException;
        }
    }

    public readonly struct NoneResult
    {
    }

    public readonly struct DelayedValue<T>
    {
        [AllowNull]
        public T Value { get; }

        public DelayedValue([AllowNull] T value)
        {
            Value = value;
        }
    }

    public readonly struct DelayedError<T>
    {
        [AllowNull]
        public T Value { get; }

        public DelayedError([AllowNull] T value)
        {
            Value = value;
        }
    }

    public static class Result
    {
        public static DelayedValue<TOk> Ok<TOk>(TOk ok)
        {
            return new DelayedValue<TOk>(ok);
        }

        public static DelayedError<TError> Error<TError>(TError error)
        {
            return new DelayedError<TError>(error);
        }
    }
 }
