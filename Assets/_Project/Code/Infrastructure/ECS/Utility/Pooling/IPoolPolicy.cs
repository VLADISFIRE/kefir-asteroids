namespace Infrastructure.ECS
{
    public interface IPoolPolicy<T>
    {
        public void OnGet(ref T obj);
        public void OnRelease(ref T obj);
    }
}