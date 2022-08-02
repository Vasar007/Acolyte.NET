using System.Collections.Generic;
using System.Linq;
using Acolyte.Assertions;

namespace Acolyte.Common
{
    public sealed class ExpendableFormatString
    {
        private readonly string _format;

        private readonly IEnumerable<object> _initialArgs;


        public ExpendableFormatString(
            string format,
            IEnumerable<object> initialArgs)
        {
            _format = format.ThrowIfNull(nameof(format));
            _initialArgs = initialArgs.ThrowIfNull(nameof(initialArgs));
        }

        public ExpendableFormatString(
            string format,
            params object[] initialArgs)
            : this(format, initialArgs.ThrowIfNull(nameof(initialArgs)).AsEnumerable())
        {
        }

        public string Format(IEnumerable<object> additionalArgs)
        {
            var args = _initialArgs.Concat(additionalArgs);
            return FormatInternal(args);
        }

        public string Format(params object[] additionalArgs)
        {
            return Format(additionalArgs.AsEnumerable());
        }

        private string FormatInternal(IEnumerable<object> args)
        {
            return string.Format(_format, args.ToArray());
        }
    }
}
