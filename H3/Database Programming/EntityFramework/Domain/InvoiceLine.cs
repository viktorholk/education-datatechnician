using Domain.Common;

namespace Domain
{
    public class InvoiceLine : BaseDomainObject
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public double PriceWithoutVAT { get; set; }

        public int OrderLineId { get; set; }
        public OrderLine OrderLine { get; set; }

        public int AmountDelivered { get; set; }
        public int AmountBackOrder { get; set; }

        public override string ToString()
        {
            return $"Invoice Id = {InvoiceId} OrderLine = {OrderLineId} {PriceWithoutVAT} {AmountDelivered} {AmountBackOrder}";
        }

    }
}
