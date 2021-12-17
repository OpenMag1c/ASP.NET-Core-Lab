namespace DAL.Interfaces
{
    public interface ICachingData<T> where T : class
    {
        bool CheckCacheData(string key, out T data);
        void SetCacheData(string key, T data);
        void RemoveCacheData(string key);
    }
}