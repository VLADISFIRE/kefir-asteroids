namespace Gameplay
{
    public class CollisionMatrix : ICollisionMatrix
    {
        public bool Check(int layer1, int layer2)
        {
            if (layer1 == CollisionLayer.ASTEROID && layer2 == CollisionLayer.ASTEROID) return false;

            return true;
        }
    }
}