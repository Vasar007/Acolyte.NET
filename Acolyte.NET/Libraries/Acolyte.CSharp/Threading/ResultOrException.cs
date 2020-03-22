using System;
using System.Diagnostics.CodeAnalysis;
using Acolyte.Assertions;

namespace Acolyte.Threading
{
    /// <summary>
    /// Represents an object with result or exception value from completed tasks.
    /// </summary>
    /// <typeparam name="T">The type of the task result.</typeparam>
    public sealed class ResultOrException<T>
    {
        [MaybeNull]
        private readonly T _result;
        
        private readonly Exception? _exception;

        public bool IsSuccess { get; }
        
        [MaybeNull]
        public T Result => IsSuccess
            ? _result
            : throw new InvalidOperationException($"{nameof(Result)} property was not active.");
        
        public Exception Exception => !IsSuccess && !(_exception is null) // Add null check to prove that we return not null value.
            ? _exception
            : throw new InvalidOperationException($"{nameof(Exception)} property was not active.");


        public ResultOrException([AllowNull] T result)
        {
            IsSuccess = true;

            // WTF, _result is marked with MayBeNullAttribute.
#pragma warning disable CS8601 // Possible null reference assignment.
            _result = result;
#pragma warning restore CS8601 // Possible null reference assignment.
            _exception = null;
        }

        public ResultOrException(Exception ex)
        {
            IsSuccess = false;
            _result = default!;
            _exception = ex.ThrowIfNull(nameof(ex));
        }

        /// <summary>
        /// Retrieves the value of the current <see cref="ResultOrException{T}" /> object, or the
        /// specified default value.
        /// </summary>
        /// <param name="defaultResult">
        /// A value to return if the <see cref="IsSuccess" /> property is <c>false</c>.
        /// </param>
        /// <returns>
        /// The value of the <see cref="Result" /> property if the <see cref="IsSuccess" /> property
        /// is <c>true</c>; otherwise, the <paramref name="defaultResult" /> parameter.
        ///</returns>
        [return: MaybeNull]
        public T GetResultOrDefault([AllowNull] T defaultResult = default!)
        {
            return IsSuccess
                ? _result
                : defaultResult;
        }

        /// <summary>
        /// Retrieves the exception value of the current <see cref="ResultOrException{T}" /> object,
        /// or the specified default exception value.
        /// </summary>
        /// <param name="defaultException">
        /// An exception value to return if the <see cref="IsSuccess" /> property is <c>true</c> and
        /// exception value is not <c>null</c>.
        /// </param>
        /// <returns>
        /// The exception value of the <see cref="Exception" /> property if the
        /// <see cref="IsSuccess" /> property is <c>false</c> and exception value is not
        /// <c>null</c>; otherwise, the <paramref name="defaultException" /> parameter.
        ///</returns>
        public Exception? GetExceptionOrDefault([AllowNull] Exception? defaultException = null)
        {
            return !IsSuccess && !(_exception is null) // Use the same check as in Exception property.
                ? _exception
                : defaultException;
        }
    }
}
