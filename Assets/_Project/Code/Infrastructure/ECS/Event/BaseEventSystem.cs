namespace Infrastructure.ECS
{
    /// <summary>
    /// Need for deleting one frame component (IEvent)... (one of the options for implementing events)
    /// </summary>
    public class BaseEventSystem<T> : BaseSystem where T : struct, IEvent
    {
        private Mask _mask;

        protected sealed override void OnInitialize()
        {
            Mask<T>().Build(out _mask);

            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
        }

        protected sealed override void OnSystemUpdate()
        {
            foreach (var entity in _mask)
            {
                entity.RemoveComponent<T>();
            }

            OnSystemUpdated();
        }

        protected virtual void OnSystemUpdated()
        {
        }
    }

    public class BaseEventSystem<T, T1> : BaseSystem
        where T : struct, IEvent
        where T1 : struct, IEvent
    {
        private Mask _mask;
        private Mask _mask2;

        protected sealed override void OnInitialize()
        {
            Mask<T>().Build(out _mask);
            Mask<T1>().Build(out _mask2);

            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
            
        }

        protected sealed override void OnSystemUpdate()
        {
            foreach (var entity in _mask)
            {
                entity.RemoveComponent<T>();
            }

            foreach (var entity in _mask2)
            {
                entity.RemoveComponent<T1>();
            }

            OnSystemUpdated();
        }

        protected virtual void OnSystemUpdated()
        {
        }
    }

    public class BaseEventSystem<T, T1, T2> : BaseSystem
        where T : struct, IEvent
        where T1 : struct, IEvent
        where T2 : struct, IEvent
    {
        private Mask _mask;
        private Mask _mask2;
        private Mask _mask3;

        protected sealed override void OnInitialize()
        {
            Mask<T>().Build(out _mask);
            Mask<T1>().Build(out _mask2);
            Mask<T2>().Build(out _mask3);

            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
        }

        protected sealed override void OnSystemUpdate()
        {
            foreach (var entity in _mask)
            {
                entity.RemoveComponent<T>();
            }

            foreach (var entity in _mask2)
            {
                entity.RemoveComponent<T1>();
            }

            foreach (var entity in _mask3)
            {
                entity.RemoveComponent<T2>();
            }

            OnSystemUpdated();
        }

        protected virtual void OnSystemUpdated()
        {
        }
    }
}