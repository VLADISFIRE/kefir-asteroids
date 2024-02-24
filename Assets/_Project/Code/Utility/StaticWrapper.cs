using System;

namespace Game.Utility
{
    public abstract class StaticWrapper<T> where T : class
    {
        protected static T _instance;

        protected static bool _instanceDisposing = true;

        public static bool initialized { get { return _instance != null; } }

        public static void Initialize(T instance, bool instanceDisposing = true)
        {
            Dispose();

            _instance = instance;

            _instanceDisposing = instanceDisposing;
        }

        public static void Dispose()
        {
            if (_instance == null) return;

            if (_instanceDisposing && _instance is IDisposable disposable)
            {
                disposable?.Dispose();
            }

            _instance = null;
        }
    }
}