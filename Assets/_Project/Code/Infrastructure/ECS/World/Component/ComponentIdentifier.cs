using System;
using System.Collections.Generic;

namespace Infrastructure.ECS
{
    internal static class ComponentIdentifier
    {
        internal static Dictionary<int, Type> typeByHash = new(32);
    }

    internal static class ComponentIdentifier<T>
    {
        internal static int hash;

        static ComponentIdentifier()
        {
            var type = typeof(T);
            hash = type.GetHashCode();
            ComponentIdentifier.typeByHash.Add(hash, type);
        }
    }
}