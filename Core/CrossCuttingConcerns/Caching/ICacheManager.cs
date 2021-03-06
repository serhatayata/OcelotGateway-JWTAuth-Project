namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object data, int duration);//duration:Keşte nekadar duracağı(dakika).
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
    }
}
