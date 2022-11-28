using Domain.Common;

namespace Domain
{
    public class Employee : BaseDomainObject
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int? CaptainId { get; set; }
        public Employee? Captain { get; set; }

        public ICollection<Employee>? Followers { get; set; }

    }
}
