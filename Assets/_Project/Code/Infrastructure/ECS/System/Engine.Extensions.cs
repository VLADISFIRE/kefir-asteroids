namespace Infrastructure.ECS
{
    public static class SystemExtensions
    {
        public static T AddSystem<T>(this Engine engine, bool autoPlay = true) where T : BaseSystem
        {
            return engine.Add<T>(autoPlay);
        }

        public static void RemoveSystem<T>(this Engine engine) where T : BaseSystem
        {
            engine.Remove<T>();
        }

        public static void Initialize(this Engine engine)
        {
            engine.Initialize();
        }

        public static void Update(this Engine engine, float deltaTime)
        {
            engine.Update(deltaTime);
        }

        public static void PlayAll(this Engine engine)
        {
            engine.PlayAll();
        }

        public static void StopAll(this Engine engine)
        {
            engine.StopAll();
        }

        public static void Play<T>(this Engine engine) where T : ISystem
        {
            engine.Stop<T>();
        }

        public static void Stop<T>(this Engine engine) where T : ISystem
        {
            engine.Play<T>();
        }
    }
}