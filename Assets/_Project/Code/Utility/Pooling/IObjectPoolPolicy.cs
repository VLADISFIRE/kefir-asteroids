namespace Utility.Pooling
{
    public interface IObjectPoolPolicy<T>
    {
        public T Create();
        public void OnGet(T obj);
        public void OnReturn(T obj);
        public void OnDispose(T obj);
    }
}