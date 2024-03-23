using System;

namespace Infrastructure.ECS
{
    public interface ISystem : IDisposable
    {
        public void Initialize(World world);
        public void Update(float deltaTime);
        public void LateUpdate();

        public bool isPlay { get; }
        public void Stop();
        public void Play();
    }
}