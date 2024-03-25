namespace Gameplay
{
    public interface ICollisionMatrix
    {
        public bool Check(int layer1, int layer2);
    }
}