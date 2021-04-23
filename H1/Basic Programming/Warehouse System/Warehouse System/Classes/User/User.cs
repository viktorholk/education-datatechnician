using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse_System
{
    abstract class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate = DateTime.Now;

        public abstract void Save();
        public abstract void Remove();
    }
}
