namespace SchadLucas.Configuration
{
    public abstract class EzConfiguration
    {
        public object Get(string key) => Get<object>(key);

        public abstract T Get<T>(string key);

        public abstract void Set(string key, object value);
    }
}