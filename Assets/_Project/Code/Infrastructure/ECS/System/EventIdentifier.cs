using System;
using System.Collections.Generic;

namespace Infrastructure.ECS
{
    internal static class EventIdentifier
    {
        internal static Dictionary<int, Type> typeByHash = new(32);
    }

    internal static class EventIdentifier<T>
    {
        internal static int hash;

        static EventIdentifier()
        {
            var type = typeof(T);
            hash = type.GetHashCode();
            EventIdentifier.typeByHash.Add(hash, type);
        }
    }
}