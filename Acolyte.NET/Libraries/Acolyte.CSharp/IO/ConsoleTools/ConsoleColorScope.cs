using System;
using Acolyte.Assertions;
using Acolyte.Common.Disposal;

namespace Acolyte.IO.ConsoleTools
{
    public sealed class ConsoleColorScope : Disposable
    {
        private readonly ConsoleColor _resetColor;

        public ConsoleColorScope(
            ConsoleColor consoleColor,
            ConsoleColor resetColor = ConsoleColor.White)
        {
            consoleColor.ThrowIfEnumValueIsUndefined(nameof(consoleColor));

            _resetColor = resetColor.ThrowIfEnumValueIsUndefined(nameof(resetColor));
            Console.ForegroundColor = consoleColor;
        }

        #region IDisposable Implementation

        protected override void DisposeInternal()
        {
            Console.ForegroundColor = _resetColor;
        }

        #endregion
    }
}
