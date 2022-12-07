using Domain.Common;

namespace Domain
{
    public class Address : BaseDomainObject
    {

        public string Address1 { get; set; }
        public string? Address2 { get; set; }

        public string City { get; set; }
        public int PostalCode { get; set; }

        public ICollection<Address>? InvoiceAddresses { get; set; }
        public ICollection<Address>? DeliveryAddresses { get; set; }

    }
}
