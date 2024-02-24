namespace Infrastructure.ECS
{
    public abstract partial class BaseSystem : ISystem
    {
        protected World _world;

        public void Initialize(World world)
        {
            _world = world;
            
            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }

        public void Update(float deltaTime)
        {
            OnUpdate(deltaTime);
        }

        protected virtual void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        public void LateUpdate()
        {
            OnLateUpdate();
        }

        protected virtual void OnLateUpdate()
        {
        }
    }
}