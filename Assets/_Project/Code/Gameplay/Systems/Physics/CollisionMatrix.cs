namespace Gameplay
{
    public class CollisionMatrix : ICollisionMatrix
    {
        public bool Check(int layer1, int layer2)
        {
            if (layer1 == CollisionLayer.ENEMY && layer2 == CollisionLayer.ENEMY) return false;
            if (layer1 == CollisionLayer.ROCKET && layer2 == CollisionLayer.ROCKET) return false;

            return true;
        }
    }
}