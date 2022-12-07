using Domain.Common;

namespace Domain
{

    public class Department : BaseDomainObject
    {
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }

}





