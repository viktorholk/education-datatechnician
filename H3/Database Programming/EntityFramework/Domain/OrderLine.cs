using Domain.Common;

namespace Domain
{
    public class OrderLine : BaseDomainObject
    {

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int Amount { get; set; }
        public double Discount { get; set; }

        public ICollection<InvoiceLine> InvoiceLines { get; set; }

        public override string ToString()
        {
            return $"{Id}, Product ID = {ProductId} Order Id = {OrderId}, {Amount} {Discount}";
        }
    }
}

