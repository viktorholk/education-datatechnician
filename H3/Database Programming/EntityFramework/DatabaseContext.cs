using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using F = Faker;
using Domain;
using Domain.Common;

public class DatabaseContext : DbContext
{

    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceLine> InvoiceLines { get; set; }


    public override int SaveChanges()
    {
        UpdateEntityMetaData();

        return base.SaveChanges();
    }

    public int SaveChanges(int employeeId)
    {
        UpdateEntityMetaData(employeeId);

        return base.SaveChanges();
    }

    public void Seed(bool clean = true)
    {

        if (clean)
        {
            Database.ExecuteSqlRaw($"PRAGMA foreign_keys = 0;");

            foreach (string tableName in new string[] { "Departments", "Employees", "Addresses", "Customers", "Orders", "OrderLines", "Invoices", "InvoiceLines", "Products" })
            {
                Database.ExecuteSqlRaw($"DELETE FROM {tableName};");
            }

            this.SaveChanges();
            Database.ExecuteSqlRaw($"PRAGMA foreign_keys = 1;");
        }

        using (var transaction = Database.BeginTransaction())
        {
            try
            {
                Department developerDepartment = new Department { Name = "Developers" };
                Employee captain = new Employee { Name = F.Name.FullName(), Department = developerDepartment };

                List<Employee> employees = new()  {
                        new Employee { Name = F.Name.First() },
                        new Employee { Name = F.Name.First() },
                        new Employee { Name = F.Name.First() },
                        new Employee { Name = F.Name.First() },
                };

                this.Employees.Add(captain);

                employees.ForEach(e =>
                {
                    e.Department = developerDepartment;
                    e.Captain = captain;
                    this.Employees.Add(e);
                });


                Address invoiceAddress = new Address { Address1 = F.Address.StreetAddress(), Address2 = F.Address.CitySuffix(), City = F.Address.City(), PostalCode = 1234 };
                Address deliveryAddress = new Address { Address1 = F.Address.StreetAddress(), Address2 = F.Address.CitySuffix(), City = F.Address.City(), PostalCode = 1234 };
                this.Addresses.Add(invoiceAddress);
                this.Addresses.Add(deliveryAddress);

                var rand = new Random();

                for (int lightweightbaby = 0; lightweightbaby < rand.Next(3, 4); lightweightbaby++)
                {
                    Customer customer = new Customer { CVR = "12345678", Name = F.Company.Name(), VATRate = 0.25, ContactPerson = F.Name.FullName(), InvoiceAddress = invoiceAddress, DeliveryAddress = deliveryAddress };
                    this.Customers.Add(customer);
                    for (int i = 0; i < rand.Next(4, 6); i++)
                    {
                        var order = new Order { Customer = customer };
                        this.Orders.Add(order);

                        var invoice = new Invoice { Order = order, PriceWithVAT = 100, PriceWithoutVAT = 75, PaymentDate = DateTime.Now.AddDays(5) };
                        this.Invoices.Add(invoice);

                        // Add lines

                        for (int j = 0; j < rand.Next(2, 4); j++)
                        {
                            var product = new Product { Name = F.Finance.Credit.BondName(), Unit = "Unit", PricePerUnit = rand.NextDouble(), WarehouseCount = rand.Next(1, 10) };
                            this.Products.Add(product);

                            var orderLine = new OrderLine { Product = product, Order = order, Amount = rand.Next(1, 100), Discount = rand.NextDouble() * 100 };
                            this.OrderLines.Add(orderLine);


                            for (int k = 0; k < rand.Next(1, 2); k++)
                            {
                                var invoiceLine = new InvoiceLine { Invoice = invoice, PriceWithoutVAT = rand.NextDouble() * 100, OrderLine = orderLine, AmountDelivered = rand.Next(0, 5), AmountBackOrder = rand.Next(0, 5) };
                                this.InvoiceLines.Add(invoiceLine);
                            }
                        }
                    }
                }
                this.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=db.sqlite3");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvoiceLine>(context =>
        {
            context.HasOne(i => i.Invoice)
                .WithMany(i => i.InvoiceLines)
                .HasForeignKey(i => i.InvoiceId);

            context.HasOne(i => i.OrderLine)
                .WithMany(o => o.InvoiceLines);
        });

        modelBuilder.Entity<OrderLine>(context =>
        {
            context.HasOne(o => o.Product)
                .WithMany(p => p.OrderLines)
                .HasForeignKey(o => o.ProductId);

            context.HasOne(o => o.Order)
                .WithMany(o => o.OrderLines)
                .HasForeignKey(o => o.OrderId);

        });

        modelBuilder.Entity<Invoice>(context =>
        {
            context.HasOne(i => i.Order)
                .WithMany(o => o.Invoices);
        });

        modelBuilder.Entity<Invoice>(context =>
        {
            context.HasOne(i => i.Order)
                .WithMany(o => o.Invoices);
        });


        modelBuilder.Entity<Order>(context =>
        {
            context.HasOne(o => o.Customer)
                .WithMany(c => c.Orders);
        });

        modelBuilder.Entity<Customer>(context =>
        {
            context.HasOne(c => c.InvoiceAddress);
            context.HasOne(c => c.DeliveryAddress);
        });

        modelBuilder.Entity<Department>(context =>
        {
            context.HasMany(d => d.Employees)
                .WithOne(e => e.Department);
        });

        modelBuilder.Entity<Employee>(context =>
        {
            context.HasOne(e => e.Department)
                .WithMany(d => d.Employees);

            context.HasOne(e => e.Captain)
                .WithMany(e => e.Followers)
                .HasForeignKey(e => e.CaptainId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }


    private void UpdateEntityMetaData(int employeeId = -1)
    {

        // Get employee
        Employee? employee = null;

        if (employeeId > 0)
            employee = Employees.Find(employeeId);

        DateTime now = DateTime.UtcNow;

        var entries = ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            var entity = (BaseDomainObject)entry.Entity;

            switch (entry.State)
            {
                case EntityState.Added:
                    entity.CreatedDate = now;
                    if (employee is not null)
                        entity.CreatedBy = employee.Name;
                    break;
                case EntityState.Modified:
                    entity.ModifiedDate = now;
                    if (employee is not null)
                        entity.CreatedBy = employee.Name;
                    break;
            }
        }

    }
}
