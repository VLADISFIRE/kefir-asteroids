using UnityEngine;

namespace Infrastructure.ECS
{
    public sealed partial class World
    {
        private void OnComponentAddedByMask(int index, int hash)
        {
            foreach (var mask in _masks.Values)
            {
                mask.OnComponentAdded(index, hash);
            }
        }

        private void OnComponentRemovedByMask(int index, int hash)
        {
            foreach (var mask in _masks.Values)
            {
                mask.OnComponentRemoved(index, hash);
            }
        }

        private void OnEntityRemovedByMask(int index)
        {
            foreach (var mask in _masks.Values)
            {
                mask.Remove(index);
            }
        }
    }
}