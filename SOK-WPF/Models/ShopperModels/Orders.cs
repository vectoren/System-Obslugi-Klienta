using System;
using System.Collections.Generic;
using System.Text;

namespace SOK_WPF.Models.ShopperModels
{
    public class Orders
    {
        public int? orderId { get; set; } = null;
        public string products { get; set; }
        public decimal wholeCost { get; set; }
        public string orderDate { get; set; }

        public Orders(string products, decimal wholeCost, string orderDate)
        {
            this.products = products;
            this.wholeCost = wholeCost;
            this.orderDate = orderDate;
        }


    }
}
