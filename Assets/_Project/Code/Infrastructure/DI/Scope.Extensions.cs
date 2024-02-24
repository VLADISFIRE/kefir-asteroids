using System;

namespace Infrastructure.DI
{
    public static class Extensions
    {
        public static void Register<T>(this Scope scope, out T instance)
        {
            instance = scope.Register<T, T>();
        }
        
        public static T Register<T>(this Scope scope)
        {
            return scope.Register<T, T>();
        }

        /// <summary>
        /// Create instance + resolving => register
        /// </summary>
        public static TImplementation Register<TContract, TImplementation>(this Scope scope)
            where TImplementation : TContract
        {
            var type = typeof(TImplementation);

            if (scope.IsAlreadyRegister(type))
            {
                throw new Exception($"Already registered [ {type.Name} ]!");
            }

            var instance = Utility.CreateInstance<TImplementation>(scope, type);

            return scope.RegisterInstance<TContract, TImplementation>(instance);
        }

        public static T RegisterInstance<T>(this Scope scope, T instance)
        {
            return scope.RegisterInstance<T, T>(instance);
        }

        public static TImplementation RegisterInstance<TContract, TImplementation>(this Scope scope, TImplementation instance)
            where TImplementation : TContract
        {
            scope.Register<TContract>(instance);
            return instance;
        }

        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length <= 0;
        }

        public static void Terminate(this Scope scope)
        {
            scope.Dispose();
        }
    }
}