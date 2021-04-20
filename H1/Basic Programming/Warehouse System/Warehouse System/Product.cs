using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse_System
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitSize { get; set; }

        public Product(int id, string name, int unitSize)
        {
            this.Id = id;
            this.Name = name;
            this.UnitSize = unitSize;
        }

        public override string ToString()
        {
            return $"{this.Id}, {this.Name}, {this.UnitSize}";
        }
    }
}
