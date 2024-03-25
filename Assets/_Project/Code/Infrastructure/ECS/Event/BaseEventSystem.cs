using System.Collections.Generic;

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
            for (int i = _mask.count; i-- > 0;)
            {
                _mask[i].RemoveComponent<T>();
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
        private List<Mask> _masks = new();

        protected sealed override void OnInitialize()
        {
            _masks.Add(Mask<T>().Build());
            _masks.Add(Mask<T1>().Build());

            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
        }

        protected sealed override void OnSystemUpdate()
        {
            for (int i = 0; i < _masks.Count; i++)
            {
                for (int j = _masks[i].count; j-- > 0;)
                {
                    _masks[i][j].RemoveComponent<T>();
                }
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
        private List<Mask> _masks = new();

        protected sealed override void OnInitialize()
        {
            _masks.Add(Mask<T>().Build());
            _masks.Add(Mask<T1>().Build());
            _masks.Add(Mask<T2>().Build());

            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
        }

        protected sealed override void OnSystemUpdate()
        {
            for (int i = 0; i < _masks.Count; i++)
            {
                for (int j = _masks[i].count; j-- > 0;)
                {
                    _masks[i][j].RemoveComponent<T>();
                }
            }

            OnSystemUpdated();
        }

        protected virtual void OnSystemUpdated()
        {
        }
    }
}