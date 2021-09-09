using System;
using Acolyte.Assertions;

namespace Acolyte.IO.ConsoleTools
{
    public sealed class ConsoleColorScope : IDisposable
    {
        private readonly ConsoleColor _resetColor;

        public ConsoleColorScope(ConsoleColor consoleColor, ConsoleColor resetColor = ConsoleColor.White)
        {
            consoleColor.ThrowIfEnumValueIsUndefined(nameof(consoleColor));

            _resetColor = resetColor.ThrowIfEnumValueIsUndefined(nameof(resetColor));
            Console.ForegroundColor = consoleColor;
        }

        #region IDisposable Members

        private bool _disposed;

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Console.ForegroundColor = _resetColor;

            _disposed = true;
        }

        #endregion
    }
}
