using System;
using System.Collections.Generic;
using System.Text;
using Warehouse_System.Classes.SQL;
using System.Linq;
using Warehouse_System.Classes.Application;
namespace Warehouse_System.Classes.Warehouse
{
    class Product : SQLObject
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int UnitSize { get; set; }
        public ProductCategory Category;

        public new bool Saved
        {
            get         { return base.Saved; }
            private set { base.Saved = value; }
        }

        public Product(string name, ProductCategory category, int unitSize, int unitPrice)
        {
            this.Name = name;
            this.UnitSize = unitSize;
            this.UnitPrice = unitPrice;
            this.Category = category;
        }
        public Product(string id, string name, string categoryId, string unitSize, string unitPrice, string shelfId)
        {
            this.Id = Convert.ToInt32(id);
            this.Name = name;
            this.Category = ProductCategory.categories.Single(i => i.Id == Convert.ToInt32(categoryId));
            this.UnitSize = Convert.ToInt32(unitSize);
            this.UnitPrice = Convert.ToInt32(unitPrice);
            Saved = true;
        }

        public void Save(int shelfId)
        {
            base.Insert($@" INSERT INTO products
                            (name, category_id, unitSize, unitPrice, shelf_id)
                            VALUES ('{this.Name}',{this.Category.Id},{this.UnitSize}, {this.UnitPrice}, {shelfId})");
            StatusHandler.Write($"Saved product {this.Name}", StatusHandler.Codes.SUCCESS);
        }
        public void Remove()
        {
            base.Delete($@" DELETE FROM products
                            WHERE id = {this.Id}");
            StatusHandler.Write($"Deleted product {this.Name}", StatusHandler.Codes.SUCCESS);

        }

        public override string ToString()
        {
            return $"{this.Id}, {this.Name}, {this.Category.Name}, {this.UnitSize}";
        }
    }
}
