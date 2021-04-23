using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Warehouse_System
{
    class Orderline
    {
        public int Id { get; set; }

        public Product product;
        public ProductCategory ProductCategory;

        public Orderline(int productId)
        {
            this.product = Product.products.Single(p => p.Id == productId);
            this.ProductCategory = product.Category;
        }

        public override string ToString()
        {
            return $"{this.product.Id}, {this.product.Name}, {this.product.Category.Name}, {this.product.UnitSize}, {this.product.UnitPrice}";
        }
    }
}
