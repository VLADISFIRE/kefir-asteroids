using System;
using System.Collections.Generic;

namespace Infrastructure.ECS
{
    internal static class EventIdentifier
    {
        internal static Dictionary<int, Type> typeByHash = new(32);

        internal static int Generate(Type type)
        {
            var hash = type.GetHashCode();

            if (!typeByHash.ContainsKey(hash))
                typeByHash.Add(hash, type);

            return hash;
        }
    }

    internal static class EventIdentifier<T>
    {
        internal static int hash;

        static EventIdentifier()
        {
            EventIdentifier.Generate(typeof(T));
        }
    }
}