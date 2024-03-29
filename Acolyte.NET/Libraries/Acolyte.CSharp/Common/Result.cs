﻿using System;

namespace Acolyte.Common
{
    public readonly struct Result<TOk, TError> : IEquatable<Result<TOk, TError>>
    {
        private readonly TOk? _ok;

        private readonly TError? _error;

        public bool IsSuccess { get; }

        public TOk? Ok => IsSuccess
            ? _ok
            : throw new InvalidOperationException($"{nameof(Ok)} property was not active.");

        public TError? Error => !IsSuccess
            ? _error
            : throw new InvalidOperationException($"{nameof(Error)} property was not active.");


        private Result(
            TOk? ok,
            TError? error,
            bool isSuccess)
        {
            _ok = ok;
            _error = error;
            IsSuccess = isSuccess;
        }

        public Result(
            TOk? ok)
            : this(ok, default, true)
        {
        }

        public Result(
            TError? error)
            : this(default, error, false)
        {
        }

        #region Object Overridden Methods

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is Result<TOk, TError> other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return IsSuccess
                ? System.HashCode.Combine(_ok)
                : System.HashCode.Combine(_error);
        }

        #endregion

        #region IEquatable<Result<TOk, TError>> Implementation

        /// <inheritdoc />
        public bool Equals(Result<TOk, TError> other)
        {
            if (!IsSuccess.Equals(other.IsSuccess)) return false;

            if (IsSuccess)
            {
                return _ok is null
                    ? other._ok is null
                    : _ok.Equals(other._ok);
            }

            return _error is null
                ? other._error is null
                : _error.Equals(other._error);
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
        /// <returns>
        /// <see langword="true" /> if values are memberwise equals; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public static bool operator ==(Result<TOk, TError> left, Result<TOk, TError> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="Result{TValue, TError}" /> are
        /// not equal.
        /// </summary>
        /// <param name="left">Left hand side object to compare.</param>
        /// <param name="right">Right hand side object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if values are not memberwise equals; otherwise,
        /// <see langword="false" />.
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
        /// A value to return if the <see cref="IsSuccess" /> property is <see langword="true" />.
        /// </param>
        /// <returns>
        /// The value of the <see cref="Result" /> property if the <see cref="IsSuccess" /> property
        /// is <see langword="false" />; otherwise, the <paramref name="defaultValue" /> parameter.
        ///</returns>
        public TOk? GetValueOrDefault(TOk? defaultValue = default)
        {
            return IsSuccess
                ? _ok
                : defaultValue;
        }

        /// <summary>
        /// Retrieves the exception value of the current <see cref="Result{TOk, TError}" /> object,
        /// or the specified default exception value.
        /// </summary>
        /// <param name="defaultException">
        /// An exception value to return if the <see cref="IsSuccess" /> property is
        /// <see langword="false" /> and exception value is not <see langword="null" />.
        /// </param>
        /// <returns>
        /// The exception value of the <see cref="Exception" /> property if the
        /// <see cref="IsSuccess" /> property is <see langword="true" />; otherwise, the
        /// <paramref name="defaultException" /> parameter.
        ///</returns>
        public TError? GetExceptionOrDefault(TError? defaultException = default)
        {
            return !IsSuccess
                ? _error
                : defaultException;
        }

        public TOut Match<TOut>(Func<TOk?, TOut> ok, Func<TError?, TOut> error)
        {
            return IsSuccess
                ? ok(_ok)
                : error(_error);
        }

        public void Match(Action<TOk?> ok, Action<TError?> error)
        {
            if (IsSuccess)
            {
                ok(_ok);
            }
            else
            {
                error(_error);
            }
        }
    }

    public readonly struct NoneResult
    {
    }

    public readonly struct DelayedValue<T>
    {
        public T? Value { get; }

        public DelayedValue(T? value)
        {
            Value = value;
        }
    }

    public readonly struct DelayedError<T>
    {
        public T? Value { get; }

        public DelayedError(T? value)
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
