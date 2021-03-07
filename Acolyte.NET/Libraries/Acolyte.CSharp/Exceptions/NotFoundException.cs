using System;
using System.Runtime.Serialization;

namespace Acolyte.Exceptions
{
    /// <summary>
    /// The exception that is thrown when failed to find object.
    /// </summary>
    [Serializable]
    public sealed class NotFoundException : Exception
    {
        /// <summary>
        /// Creates the exception.
        /// </summary>
        public NotFoundException()
        {
        }

        /// <summary>
        /// Creates the exception with description.
        /// </summary>
        /// <param name="message">The exception description.</param>
        public NotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates the exception with description and inner cause.
        /// </summary>
        /// <param name="message">The exception description.</param>
        /// <param name="innerException">The exception inner cause.</param>
        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
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
        private NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Creates the exception with description using provided object name.
        /// </summary>
        /// <param name="objectName">The object name failed to find.</param>
        /// <returns>Exception with formated message.</returns>
        public static NotFoundException Create(string objectName)
        {
            return new NotFoundException($"Failed to find '{objectName}'.");
        }

        /// <summary>
        /// Creates the exception with description using provided object name and inner cause.
        /// </summary>
        /// <param name="objectName">The object name failed to find.</param>
        /// <param name="innerException"></param>
        /// <returns>Exception with formated message and inner cause.</returns>
        public static NotFoundException Create(string objectName, Exception innerException)
        {
            return new NotFoundException($"Failed to find '{objectName}'.", innerException);
        }
    }
}
