using Shopper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.UsageClasses
{
    public interface IDataCache
    {
        void AddData(Product key, int value = 1);
        void DeleteData(Product key);
        void ClearCache();
        (Product, int) GetData(Product key);
        Dictionary<Product, int> GetAllData();
    }
}
