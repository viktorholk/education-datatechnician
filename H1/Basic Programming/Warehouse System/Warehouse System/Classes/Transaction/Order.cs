using System;
using System.Collections.Generic;
using System.Text;

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
        public override void Save()
        {

        }
        public override void Remove()
        {

        }
    }
}
