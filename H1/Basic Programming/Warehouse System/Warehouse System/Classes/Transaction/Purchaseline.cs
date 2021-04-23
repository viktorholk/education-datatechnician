using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse_System
{
    class Purchaseline
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public Purchaseline(string name, int unitPrice)
        {
            this.Name = name;
            this.UnitPrice = unitPrice;
        }
    }
}
