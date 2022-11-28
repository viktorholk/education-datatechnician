using Domain.Common;

namespace Domain
{
    public class Customer : BaseDomainObject
    {
        public string CVR { get; set; }
        public string Name { get; set; }
        public double VATRate { get; set; }
        public string ContactPerson { get; set; }

        public int InvoiceAddressId { get; set; }
        public Address InvoiceAddress { get; set; }

        public int DeliveryAddressId { get; set; }
        public Address DeliveryAddress { get; set; }

        public ICollection<Order> Orders { get; set; }


        public override string ToString()
                {
                    return $"{Id}, {CVR} {Name} {VATRate} {ContactPerson}";
                }


    }
}
