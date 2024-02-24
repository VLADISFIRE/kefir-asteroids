using System;
using System.Reflection;

namespace Infrastructure.DI
{
    public static class Utility
    {
        internal static T CreateInstance<T>(Scope scope, Type type)
        {
            T instance;
            var types = GetTypes(type);

            if (!types.IsNullOrEmpty())
            {
                var value = Resolve(scope, types);

                instance = (T) Activator.CreateInstance(type, value);
            }
            else
            {
                instance = Activator.CreateInstance<T>();
            }

            return instance;
        }

        internal static Type[] GetTypes(Type type)
        {
            var infos = type.GetConstructors();

            ParameterInfo[] parameters = null;

            for (int i = 0; i < infos.Length; i++)
            {
                var x = infos[i].GetParameters();
                if (parameters == null || x.Length > parameters.Length)
                {
                    parameters = x;
                }
            }

            return parameters != null ? GetTypes(parameters) : null;
        }

        internal static Type[] GetTypes(ParameterInfo[] parameters)
        {
            var length = parameters.Length;
            var types = new Type[length];

            for (int i = 0; i < length; i++)
            {
                types[i] = parameters[i].ParameterType;
            }

            return types;
        }

        private static object[] Resolve(Scope scope, Type[] types)
        {
            object[] args = null;

            for (int j = 0; j < types.Length; j++)
            {
                if (args == null)
                {
                    args = new object[types.Length];
                }

                var argType = types[j];

                if (!scope.TryResolve(argType, out var arg))
                {
                    throw new Exception($"Can't resolve dependency by type [ {argType.Name} ]!");
                }

                args[j] = arg;
            }

            return args;
        }
    }
}