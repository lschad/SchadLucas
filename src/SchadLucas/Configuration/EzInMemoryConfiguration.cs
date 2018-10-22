using System;
using System.Collections.Generic;

namespace SchadLucas.Configuration
{
    public sealed class EzInMemoryConfiguration : EzConfiguration
    {
        private readonly Dictionary<string, object> _storage = new Dictionary<string, object>();

        public override T Get<T>(string key)
        {
            if (!HasKey(key))
            {
                throw new ArgumentException($"Configuration does not contain key {key}");
            }

            return (T) _storage[key];
        }

        public bool HasKey(string key)
        {
            return _storage.ContainsKey(key);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key) || !HasKey(key))
            {
                throw new ArgumentException("Key is not valid.", nameof(key));
            }

            _storage.Remove(key);
        }

        public override void Set(string key, object value)
        {
            if (!HasKey(key))
            {
                _storage.Add(key, null);
            }

            _storage[key] = value;
        }
    }
}