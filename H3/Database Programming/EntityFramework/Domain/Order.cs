using Domain.Common;

namespace Domain
{
    public class Order : BaseDomainObject
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }


        public override string ToString()
        {
            return $"{Id}, Customer = {Customer}";
        }

    }
}

