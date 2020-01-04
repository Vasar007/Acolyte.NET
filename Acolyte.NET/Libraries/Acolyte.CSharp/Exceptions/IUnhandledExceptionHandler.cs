using System;

namespace Acolyte.Exceptions
{
    public interface IUnhandledExceptionHandler
    {
        void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e);
    }
}
