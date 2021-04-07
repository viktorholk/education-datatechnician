using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse_System
{

    class Product
    {
        public enum Categories
        {
            Electronics = 1,
            Agriculture = 2,
            Convenience = 3
        };

        public string       Name;
        public Categories   Category;
        public int          UnitSize;

        public Product(string name, Categories category, int unitSize)
        {
            this.Name = name;
            this.Category = category;
            this.UnitSize = unitSize;
            
        }

    }
}
