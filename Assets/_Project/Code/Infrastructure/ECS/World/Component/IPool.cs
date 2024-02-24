namespace Infrastructure.ECS
{
    internal interface IPool
    {
        public void Release(int index);
    }
}