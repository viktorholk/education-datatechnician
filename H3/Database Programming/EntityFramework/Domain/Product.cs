using Domain.Common;

namespace Domain
{
    public class Product : BaseDomainObject
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public double PricePerUnit { get; set; }

        public int WarehouseCount { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }



    }
}

