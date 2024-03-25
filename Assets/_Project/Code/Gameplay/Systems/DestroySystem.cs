using Infrastructure.ECS;

namespace Gameplay
{
    public class DestroySystem : BaseEventSystem<DestroyEvent>
    {
        private Mask _mask;

        protected override void OnInitialized()
        {
            Mask<DestroyEvent>().Build(out _mask);
        }

        protected override void OnLateUpdate()
        {
            foreach (var entity in _mask)
            {
                entity.Remove();
            }
        }
    }
}