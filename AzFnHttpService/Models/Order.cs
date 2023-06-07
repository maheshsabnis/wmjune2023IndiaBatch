using System;
using System.Collections.Generic;

namespace AzFnHttpService.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string OrderedItem { get; set; }
    }
}
