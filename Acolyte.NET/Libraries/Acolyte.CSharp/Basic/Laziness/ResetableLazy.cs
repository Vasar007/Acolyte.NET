// Copyright (c) Veeam Software Group GmbH

using System;
using System.Threading;
using Acolyte.Assertions;
using Acolyte.Basic.Disposal;

namespace Acolyte.Basic.Laziness
{
    public sealed class ResetableLazy<T>
    {
        private const LazyThreadSafetyMode DefaultMode = LazyThreadSafetyMode.ExecutionAndPublication;

        private readonly object _lock;
        private readonly Func<T> _valueFactory;
        private readonly LazyThreadSafetyMode _mode;
        private readonly Action<T>? _valueChecker;
        private readonly Action<T> _disposeAction;

        private Lazy<T> _lazy;


        public ResetableLazy(
            Func<T> valueFactory)
            : this(valueFactory, DefaultMode)
        {
        }

        public ResetableLazy(
            Func<T> valueFactory,
            bool isThreadSafe)
            : this(valueFactory, GetModeFromBoolean(isThreadSafe), valueChecker: null, DefaultDisposeAction)
        {
        }

        public ResetableLazy(
            Func<T> valueFactory,
            LazyThreadSafetyMode mode)
            : this(valueFactory, mode, valueChecker: null, DefaultDisposeAction)
        {
        }

        public ResetableLazy(
            Func<T> valueFactory,
            LazyThreadSafetyMode mode,
            Action<T>? valueChecker)
            : this(valueFactory, mode, valueChecker, DefaultDisposeAction)
        {
        }

        public ResetableLazy(
            Func<T> valueFactory,
            LazyThreadSafetyMode mode,
            Action<T>? valueChecker,
            Action<T> disposeAction)
        {
            _lock = new object();

            _valueFactory = valueFactory.ThrowIfNull(nameof(valueChecker));
            _mode = mode.ThrowIfUndefined(nameof(mode));
            _valueChecker = valueChecker;
            _disposeAction = disposeAction.ThrowIfNull(nameof(disposeAction));

            _lazy = CreateLazy(valueFactory);
        }

        public void Reset()
        {
            lock (_lock)
            {
                if (IsValueCreated)
                    _disposeAction(Value);

                _lazy = CreateLazy(_valueFactory);
            }
        }

        public T Value
        {
            get
            {
                lock (_lock)
                    return GetProtectedValue();
            }
            set
            {
                _valueChecker?.Invoke(value);

                lock (_lock)
                    _lazy = CreateLazy(() => value);
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

        private static LazyThreadSafetyMode GetModeFromBoolean(bool isThreadSafe)
        {
            return isThreadSafe
                ? DefaultMode
                : LazyThreadSafetyMode.None;
        }

        private Lazy<T> CreateLazy(Func<T> valueFactory)
        {
            return new Lazy<T>(valueFactory, _mode);
        }

        private static void DefaultDisposeAction<TIn>(TIn value)
        {
            (value as IDisposable).DisposeSafe();
        }
    }
}
