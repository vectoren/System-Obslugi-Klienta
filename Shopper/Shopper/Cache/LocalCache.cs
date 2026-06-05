using Shopper.Models;
using Shopper.UsageClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.Cache
{
    public class LocalCache : IDataCache
    {
        private readonly Dictionary<Product, int> _cache = new Dictionary<Product, int>();

        public void AddData(Product key, int value = 1)
        {
            if( _cache.ContainsKey(key))
            {
                _cache[key] = _cache[key] + 1;
            }
            else
            {
                _cache.Add(key, value);
            }
        }

        public void ClearCache()
        {
            _cache.Clear();
        }

        public void DeleteData(Product key)
        {
            _cache.Remove(key);
        }

        public Dictionary<Product, int> GetAllData()
        {
            return _cache;
        }

        public (Product, int) GetData(Product key)
        {
            return _cache.TryGetValue(key, out int value) ? (key, value) : default;
        }

    }
}