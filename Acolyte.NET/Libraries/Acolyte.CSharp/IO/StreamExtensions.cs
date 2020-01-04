using System;
using System.IO;
using Acolyte.Assertions;

namespace Acolyte.IO
{
    internal static class StreamExtensions
    {
        public static void SaveToFile(this Stream stream, string path)
        {
            SaveToFile(stream, path, overwrite: false);
        }

        public static void SaveToFile(this Stream stream, string path, bool overwrite)
        {
            SaveToFile(stream, path, overwrite, seekToBegin: false);
        }

        public static void SaveToFile(this Stream stream, string path, bool overwrite,
            bool seekToBegin)
        {
            stream.ThrowIfNull(nameof(stream));

            FileMode mode = overwrite
                ? FileMode.Create
                : FileMode.CreateNew;

            using var str = new FileStream(path, mode, FileAccess.Write);

            if (seekToBegin)
            {
                if (!stream.CanSeek)
                {
                    throw new NotSupportedException(
                        "Provided stream does not support seeking operation."
                    );
                }
                stream.Seek(0, SeekOrigin.Begin);
            }

            stream.CopyTo(str);
        }
    }
}
