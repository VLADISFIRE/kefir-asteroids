namespace Infrastructure.ECS
{
    public enum EngineType
    {
        Default,
        Fixed
    }

    public interface IEcsManager
    {
        public bool IsPlaying(EngineType type);

  

        public void SetDefaultType(EngineType type);
        
        /// <summary>
        /// Add system by default type;
        /// </summary>
        public IEcsManager AddSystem<T>(bool active = true) where T : BaseSystem;
        public IEcsManager AddSystem<T>(EngineType type, bool active = true) where T : BaseSystem;
        
        /// <summary>
        /// Add system by default type;
        /// </summary>
        public IEcsManager RemoveSystem<T>() where T : BaseSystem;
        public IEcsManager RemoveSystem<T>(EngineType type) where T : BaseSystem;

        public void PlayAll();
        public void StopAll();

        public void PlayAll(EngineType type);
        public void StopAll(EngineType type);
    }
}