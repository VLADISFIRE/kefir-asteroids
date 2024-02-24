using System;

namespace Infrastructure.ECS
{
    internal interface ISystem : IDisposable
    {
        public void Initialize(World world);
        public void Update(float deltaTime);
        public void LateUpdate();
    }
}