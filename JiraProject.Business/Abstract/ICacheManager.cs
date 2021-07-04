namespace JiraProject.Business.Abstract
{
    public interface ICacheService
    {
        public string Get(string key);
        public T Get<T>(string key);
        public void Add(string key, object value, int duration);
        public bool IsKeyExist(string key);
        public void Remove(string key);

    }
}