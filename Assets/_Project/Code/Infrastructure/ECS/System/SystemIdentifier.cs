using System;
using System.Collections.Generic;

namespace Infrastructure.ECS
{
    internal static class SystemIdentifier
    {
        internal static readonly Dictionary<int, Type> typesByHash = new(32);
    }

    internal static class SystemIdentifier<T> where T : ISystem
    {
        internal static int hash;

        static SystemIdentifier()
        {
            var type = typeof(T);
            hash = type.GetHashCode();
            SystemIdentifier.typesByHash.Add(hash, type);
        }
    }
}