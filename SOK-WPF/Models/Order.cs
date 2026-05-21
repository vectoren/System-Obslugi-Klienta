using System;
using System.Collections.Generic;
using System.Text;

namespace SOK_WPF.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateOnly OrderedOn {  get; set; }
        public int ClientId { get; set; }

        public int[]? ProductsIds { get; set; }

    }
}
