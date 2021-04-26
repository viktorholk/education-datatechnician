using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Warehouse_System
{
    class Order : Transaction
    {
        public Customer Customer;
        public List<Orderline> Orderlines = new List<Orderline>();
        public Order(Customer customer)
        {
            this.Customer = customer;
        }
        public Order(int id, Customer customer)
        {
            this.Id = id;
            this.Customer = customer;
            this.Saved = true;
        }
        public override void Save()
        {
            if (!Saved)
            {
                Database.Execute($@"
                                    INSERT INTO orders
                                    (customer_id, date)
                                    VALUES ('{this.Customer.Id}', '{this.CreationDate}')");
                Console.WriteLine($"Created order for customer {this.Customer.FirstName} {this.Customer.LastName}");
            }
        }
        public override void Remove()
        {
            if (Saved)
            {
                Database.Execute($@"
                                    DELETE FROM orders
                                    WHERE id = {this.Id}");
                Console.WriteLine($"Removed order, {this.Id}");
            }
        }
        public override string ToString()
        {
            return $"{CreationDate}, {this.Customer.FirstName} {this.Customer.LastName}";
        }
    }
}
