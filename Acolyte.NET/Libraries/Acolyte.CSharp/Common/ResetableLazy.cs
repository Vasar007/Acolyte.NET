using System;
using System.Threading;
using Acolyte.Assertions;
using Acolyte.Basic.Disposal;

namespace Acolyte.Common
{
    public sealed class ResetableLazy<T>
    {
        private readonly object _lock = new object();
        private readonly Func<T> _valueFactory;
        private readonly Action<T>? _valueChecker;
        private readonly Action<T> _disposeAction;

        private Lazy<T> _lazy = default!; // Initializes in "Reset" method.

        public ResetableLazy(
            Func<T> valueFactory)
            : this(valueFactory, valueChecker: null, disposeAction: DefaultDisposeAction)
        {
        }

        public ResetableLazy(
            Func<T> valueFactory,
            Action<T> valueChecker)
            : this(valueFactory, valueChecker, DefaultDisposeAction)
        {
            _valueChecker = valueChecker;
        }

        public ResetableLazy(
            Func<T> valueFactory,
            Action<T>? valueChecker,
            Action<T> disposeAction)
        {
            _valueFactory = valueFactory.ThrowIfNull(nameof(valueFactory));
            _valueChecker = valueChecker;
            _disposeAction = disposeAction.ThrowIfNull(nameof(disposeAction));

            Reset();
        }

        public void Reset()
        {
            lock (_lock)
            {
                if (_lazy is not null)
                {
                    if (IsValueCreated)
                    {
                        _disposeAction(Value);
                    }
                }

                _lazy = new Lazy<T>(_valueFactory, isThreadSafe: true);
            }
        }

        public T Value
        {
            get
            {
                lock (_lock)
                {
                    return GetProtectedValue();
                }
            }
            set
            {
                _valueChecker?.Invoke(value);

                lock (_lock)
                {
                    _lazy = new Lazy<T>(() => value, isThreadSafe: true);
                }
            }
        }

        private T GetProtectedValue()
        {
            try
            {
                return _lazy.Value;
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
            catch (OperationCanceledException)
            {
            }

            Reset();
            return _lazy.Value;
        }

        public bool IsValueCreated
        {
            get
            {
                lock (_lock)
                    return _lazy.IsValueCreated;
            }
            set
            {
                lock (_lock)
                {
                    if (_lazy.IsValueCreated == value)
                        return;

                    if (value)
                    {
                        if (_lazy.Value is null)
                            return;
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
        }

        private static void DefaultDisposeAction<TIn>(TIn value)
        {
            if (value is IDisposable disposable)
            {
                disposable.DisposeSafe();
            }
        }
    }
}
