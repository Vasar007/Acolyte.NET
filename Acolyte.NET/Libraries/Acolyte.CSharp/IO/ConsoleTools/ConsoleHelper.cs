using System;
using System.Text;

namespace Acolyte.IO.ConsoleTools
{
    public static class ConsoleHelper
    {
        public static void SetupUnicodeEncoding()
        {
            // Setup encoding.
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
        }
    }
}
