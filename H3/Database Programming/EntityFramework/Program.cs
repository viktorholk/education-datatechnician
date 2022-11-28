// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Domain;

DatabaseContext context = new DatabaseContext();

context.Seed();

Console.Clear();
Console.WriteLine("Database Application");
Console.WriteLine("1. Show Customers");
Console.WriteLine("2. Show Orders");
Console.WriteLine("3. Show Invoices");
Console.WriteLine("4. Show Orders (Detailed)");
Console.WriteLine("5. Show Invoices (Detailed)");

while (true)
{
    var input = Console.ReadKey().Key;
    System.Console.WriteLine();

    switch (input)
    {
        case ConsoleKey.D1:
            var customers = from Customer in context.Customers select Customer;

            foreach (var customer in customers.ToList())
            {
                System.Console.WriteLine(customer);
            }

            break;
        case ConsoleKey.D2:
            var orders = from Order in context.Orders select Order;

            foreach (var order in orders.ToList())
            {
                System.Console.WriteLine(order);
            }

            break;
        case ConsoleKey.D3:
            var invoices = from Order in context.Orders select Order;

            foreach (var invoice in invoices.ToList())
            {
                System.Console.WriteLine(invoice);
            }

            break;
        case ConsoleKey.D4:
            var ordersDetailed = context.Orders
                .Include(o => o.Invoices).ThenInclude(i => i.InvoiceLines)
                .Include(o => o.OrderLines);

            foreach (var order in ordersDetailed.ToList())
            {
                System.Console.WriteLine(order);

                System.Console.WriteLine("OrderLines");
                foreach (var orderLine in order.OrderLines)
                {

                    System.Console.WriteLine($"    {orderLine}");
                }
                System.Console.WriteLine("Invoices");
                foreach (var invoice in order.Invoices)
                {
                    System.Console.WriteLine($"    {invoice}");
                }
                System.Console.WriteLine();
            }

            break;

        case ConsoleKey.D5:
            var invoicesDetailed = context.Invoices
                .Include(i => i.InvoiceLines);

            foreach (var invoice in invoicesDetailed.ToList())
            {
                System.Console.WriteLine(invoice);

                System.Console.WriteLine("InvoiceLines");
                foreach (var invoiceLine in invoice.InvoiceLines)
                {
                    System.Console.WriteLine($"    {invoiceLine}");
                }

                System.Console.WriteLine();
            }

            break;


    }
}
