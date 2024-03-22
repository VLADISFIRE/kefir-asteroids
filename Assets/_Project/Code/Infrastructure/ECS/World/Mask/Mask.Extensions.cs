using System.Collections.Generic;

namespace Infrastructure.ECS
{
    public static class MaskExtensions
    {
        public static Mask Build(this MaskBuilder builder)
        {
            return builder.Build();
        }

        public static void Build(this MaskBuilder builder, out Mask mask)
        {
            mask = builder.Build();
        }

        public static MaskBuilder Include<T>(this MaskBuilder builder)
            where T : IComponent
        {
            return builder
               .Include<T>();
        }

        public static MaskBuilder Include<T, T1>(this MaskBuilder builder)
            where T : IComponent
            where T1 : IComponent
        {
            return builder.Include<T>().Include<T1>();
        }

        public static MaskBuilder Include<T, T1, T2>(this MaskBuilder builder)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
        {
            return builder
               .Include<T>()
               .Include<T1>()
               .Include<T2>();
        }

        public static MaskBuilder Include<T, T1, T2, T3>(this MaskBuilder builder)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
        {
            return builder
               .Include<T>()
               .Include<T1>()
               .Include<T2>()
               .Include<T3>();
        }

        public static MaskBuilder Include<T, T1, T2, T3, T4>(this MaskBuilder builder)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
        {
            return builder
               .Include<T>()
               .Include<T1>()
               .Include<T2>()
               .Include<T3>()
               .Include<T4>();
        }

        public static MaskBuilder Include<T, T1, T2, T3, T4, T5>(this MaskBuilder builder)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
            where T5 : IComponent
        {
            return builder
               .Include<T>()
               .Include<T1>()
               .Include<T2>()
               .Include<T3>()
               .Include<T4>()
               .Include<T5>();
        }

        public static MaskBuilder Exclude<T>(this MaskBuilder builder)
            where T : IComponent
        {
            return builder
               .Exclude<T>();
        }

        public static MaskBuilder Exclude<T, T1>(this MaskBuilder builder)
            where T : IComponent
            where T1 : IComponent
        {
            return builder
               .Exclude<T>()
               .Exclude<T1>();
        }

        public static MaskBuilder Exclude<T, T1, T2>(this MaskBuilder builder)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
        {
            return builder
               .Exclude<T>()
               .Exclude<T1>()
               .Exclude<T2>();
        }

        public static MaskBuilder Exclude<T, T1, T2, T3>(this MaskBuilder builder)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
        {
            return builder
               .Exclude<T>()
               .Exclude<T1>()
               .Exclude<T2>()
               .Exclude<T3>();
        }

        public static MaskBuilder Exclude<T, T1, T2, T3, T4>(this MaskBuilder builder)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
        {
            return builder
               .Exclude<T>()
               .Exclude<T1>()
               .Exclude<T2>()
               .Exclude<T3>()
               .Exclude<T4>();
        }

        public static MaskBuilder Exclude<T, T1, T2, T3, T4, T5>(this MaskBuilder builder)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
            where T5 : IComponent
        {
            return builder
               .Exclude<T>()
               .Exclude<T1>()
               .Exclude<T2>()
               .Exclude<T3>()
               .Exclude<T4>()
               .Exclude<T5>();
        }

        public static bool Check(this World world, int id, IEnumerable<int> includes, IEnumerable<int> exclude)
        {
            foreach (var hash in includes)
            {
                if (!world.Has(id, hash)) return false;
            }

            foreach (var hash in exclude)
            {
                if (world.Has(id, hash)) return false;
            }

            return true;
        }
    }
}