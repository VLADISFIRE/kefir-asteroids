namespace Infrastructure.ECS
{
    public abstract partial class BaseSystem
    {
        protected MaskBuilder Mask<T>(bool include = true) 
            where T : IComponent
        {
            var builder = GetBuilder();

            return include ? builder.Include<T>() : builder.Exclude<T>();
        }

        protected MaskBuilder Mask<T,T1>(bool include = true) 
            where T : IComponent
            where T1 : IComponent
        {
            var builder = GetBuilder();

            return include ? builder.Include<T,T1>() : builder.Exclude<T,T1>();
        }

        protected MaskBuilder Mask<T,T1,T2>(bool include = true)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
        {
            var builder = GetBuilder();

            return include ? builder.Include<T,T1,T2>() : builder.Exclude<T,T1,T2>();
        }

        protected MaskBuilder Mask<T,T1,T2,T3>(bool include = true)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
        {
            var builder = GetBuilder();

            return include ? builder.Include<T,T1,T2,T3>() : builder.Exclude<T,T1,T2,T3>();
        }

        protected MaskBuilder Mask<T,T1,T2,T3,T4>(bool include = true)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
        {
            var builder = GetBuilder();

            return include ? builder.Include<T,T1,T2,T3,T4>() : builder.Exclude<T,T1,T2,T3,T4>();
        }

        protected MaskBuilder Mask<T,T1,T2,T3,T4,T5>(bool include = true)
            where T : IComponent
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
            where T5 : IComponent
        {
            var builder = GetBuilder();

            return include ? builder.Include<T,T1,T2,T3,T4,T5>() : builder.Exclude<T,T1,T2,T3,T4,T5>();
        }

        private MaskBuilder GetBuilder()
        {
            return new MaskBuilder(_world);
        }
    }
}