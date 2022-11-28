using Domain.Common;

namespace Domain
{
    public class Invoice : BaseDomainObject
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public double PriceWithVAT { get; set; }
        public double PriceWithoutVAT { get; set; }

        public DateTime PaymentDate { get; set; }

        public ICollection<InvoiceLine> InvoiceLines { get; set; }

        public override string ToString()
        {
            return $"{Id}, OrderID = {OrderId} {PriceWithVAT} {PriceWithoutVAT} {PaymentDate}";
        }
    }
}

