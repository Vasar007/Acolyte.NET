using System;

namespace Acolyte.Exceptions
{
    public sealed class ExplosiveException : Exception
    {
        public int ExplosiveIndex { get; }

        public ExplosiveException()
        {
        }

        public ExplosiveException(
            int explosiveIndex)
            : base(GetMessage(explosiveIndex))
        {
            ExplosiveIndex = explosiveIndex;
        }

        public ExplosiveException(
            string message)
            : base(message)
        {
        }

        public ExplosiveException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }

        private static string GetMessage(int explosiveIndex)
        {
            return $"Exploded on index {explosiveIndex.ToString()}.";
        }
    }
}
