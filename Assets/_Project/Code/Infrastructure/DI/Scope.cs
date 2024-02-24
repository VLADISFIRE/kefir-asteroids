using System;
using System.Collections.Generic;

namespace Infrastructure.DI
{
    /// <summary>
    /// Simple implementation IoC container without deep optimizations.
    /// Сreating instance on registration (analog "NonLazy" in extenject/zenject/vcontainer).
    /// Resolving dependencies only for services and only on constructor.
    /// </summary>
    public class Scope : IDisposable
    {
        private Scope _parent;

        private readonly Dictionary<Type, object> _objects = new();

        internal Dictionary<Type, object> objects { get { return _objects; } }

        internal event Action disposed;

        public Scope()
        {
            Register(this);
        }

        public Scope(Scope parent) : this()
        {
            _parent = parent;
            _parent.disposed += Dispose;
        }

        public void Dispose()
        {
            TryClearParent();

            disposed?.Invoke();
        }

        internal bool TryResolve(Type type, out object obj)
        {
            if (!_objects.TryGetValue(type, out obj))
            {
                if (_parent != null)
                    return _parent.TryResolve(type, out obj);
                
                return false;
            }

            return true;
        }

        internal bool IsAlreadyRegister(Type type)
        {
            if (_objects.ContainsKey(type)) 
                return true;

            return _parent?.IsAlreadyRegister(type) ?? false;
        }

        internal void Register<T>(T instance)
        {
            _objects[typeof(T)] = instance;
        }
        
        private void TryClearParent()
        {
            if (_parent == null) return;

            _parent.disposed -= Dispose;
            _parent = null;
        }
    }
}