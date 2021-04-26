using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse_System
{
    class Customer : User
    {
        private void GetOrders()
        {
            Records orderRecords = Database.GetRecords(@$"SELECT * FROM orders
                                                    WHERE customer_id = {this.Id}");
            // Initialize the list
            orders = new List<Order>();
            if (orderRecords.Count > 0)
            {
                foreach (var order in orderRecords)
                {
                    Order m_order = new Order(Convert.ToInt32(order["id"]), this);

                    Records orderLines = Database.GetRecords($@"SELECT * FROM orderlines
                                                            WHERE order_id = {order["id"]}");
                    foreach (var orderline in orderLines)
                    {
                        Dictionary<string, string> product = Database.GetRecords($@"SELECT * FROM products
                                                                                    WHERE id = {orderline["product_id"]}")[0];
                        Orderline m_orderline = new Orderline(Convert.ToInt32(product["id"]));
                        m_order.Orderlines.Add(m_orderline);
                    }
                    orders.Add(m_order);

                }
            }
        }

        public static List<Customer> customers;
        public static void LoadCustomers()
        {
            Records records = Database.GetRecords(@$"SELECT * FROM customers");
            // Initialize the list
            customers = new List<Customer>();
            if (records.Count > 0)
            {
                foreach (var record in records)
                {
                    Customer customer = new Customer(Convert.ToInt32(record["id"]), record["firstName"], record["lastName"], record["address"], Convert.ToInt32(record["zipCode"]));
                    customers.Add(customer);
                }
            }
        }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public List<Order> orders = new List<Order>();

        public Customer(string firstName, string lastName, string address, int zipCode)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.ZipCode = zipCode;
        }
        public Customer(int id, string firstName, string lastName, string address, int zipCode)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.ZipCode = zipCode;
            GetOrders();
            this.Saved = true;
        }

        public override void Save()
        {
            if (!Saved)
            {
                // Add it to the database
                Database.Execute($@"
                INSERT INTO customers
                (firstName, lastName, address, zipCode)
                VALUES ('{this.FirstName}', '{this.LastName}','{this.Address}', {this.ZipCode})
                ");
                Console.WriteLine($"Saved customer {this.FirstName} + {this.LastName}");
                this.Saved = true;
                // Load products from the database into the list
                LoadCustomers();

            }
            else Console.WriteLine($"Customer {this.FirstName} + {this.LastName} has already been saved");
        }
        public override void Remove()
        {
            if (Saved)
            {
                // Remove it to the database
                Database.Execute(@$"
                    DELETE FROM customers
                    WHERE id = {this.Id}
                ");
                Console.WriteLine($"Removed customer {this.FirstName} + {this.LastName}");
                LoadCustomers();

            }
            else Console.WriteLine($"{this.FirstName} {this.LastName} has to be saved in the database before removal");
        }

        public void CreateOrder()
        {

        }

        public override string ToString()
        {
            return $"{this.Id}, {this.FirstName} {this.LastName}, {this.Address}, {this.ZipCode}";
        }

    }
}
