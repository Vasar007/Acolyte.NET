using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Acolyte.Assertions;

namespace Acolyte.Exceptions
{
    /// <summary>
    /// The exception that is thrown when several arguments provided to a method are not valid.
    /// </summary>
    [Serializable]
    public sealed class MultipleArgumentException : Exception
    {
        /// <summary>
        /// Creates the exception.
        /// </summary>
        public MultipleArgumentException()
        {
        }

        /// <summary>
        /// Creates the exception with description.
        /// </summary>
        /// <param name="message">The exception description.</param>
        public MultipleArgumentException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates the exception with description and inner cause.
        /// </summary>
        /// <param name="message">The exception description.</param>
        /// <param name="innerException">The exception inner cause.</param>
        public MultipleArgumentException(string message, Exception? innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates the exception with description, inner cause and parameters names.
        /// </summary>
        /// <param name="message">The exception description.</param>
        /// <param name="paramNames">The names of parameters that cause exception.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="paramNames" /> parameter is <see langword="null" />.
        /// </exception>
        public MultipleArgumentException(string message, params string[] paramNames)
            : base(FormatMessage(message, paramNames))
        {
        }

        /// <summary>
        /// Creates the exception with description, inner cause and parameters names.
        /// </summary>
        /// <param name="message">The exception description.</param>
        /// <param name="paramNames">The names of parameters that cause exception.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="paramNames" /> parameter is <see langword="null" />.
        /// </exception>
        public MultipleArgumentException(string message, IEnumerable<string> paramNames)
            : base(FormatMessage(message, paramNames))
        {
        }

        /// <summary>
        /// Creates the exception with description, inner cause and parameters names.
        /// </summary>
        /// <param name="message">The exception description.</param>
        /// <param name="innerException">The exception inner cause.</param>
        /// <param name="paramNames">The names of parameters that cause exception.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="paramNames" /> parameter is <see langword="null" />.
        /// </exception>
        public MultipleArgumentException(string message, Exception? innerException,
            params string[] paramNames)
            : base(FormatMessage(message, paramNames), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the exception class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized object data about the
        /// exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual information about the
        /// source or destination.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="info" /> parameter is <see langword="null" />.
        /// </exception>
        /// <exception cref="SerializationException">
        /// The class name is null or <see cref="Exception.HResult" /> is zero (0).
        /// </exception>
        private MultipleArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Creates the exception with description, inner cause and parameters names.
        /// </summary>
        /// <param name="message">The exception description.</param>
        /// <param name="innerException">The exception inner cause.</param>
        /// <param name="paramNames">The names of parameters that cause exception.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="paramNames" /> parameter is <see langword="null" />.
        /// </exception>
        public MultipleArgumentException(string message, Exception? innerException,
            IEnumerable<string> paramNames)
            : base(FormatMessage(message, paramNames), innerException)
        {
        }

        /// <summary>
        /// Appends parameters names to the exception message.
        /// </summary>
        /// <param name="message">The exception description.</param>
        /// <param name="paramNames">The names of parameters that cause exception.</param>
        /// <returns>Formatted message with parameters names.</returns>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="paramNames" /> parameter is <see langword="null" />.
        /// </exception>
        private static string FormatMessage(string message, IEnumerable<string> paramNames)
        {
            paramNames.ThrowIfNull(nameof(paramNames));

            string formattedParamNames = string.Join(
                ", ", paramNames.Select(paramName => $"'{paramName}'")
            );

            return $"{message} (Parameters: {formattedParamNames})";
        }
    }
}
