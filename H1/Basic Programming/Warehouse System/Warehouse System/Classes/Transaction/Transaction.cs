using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse_System
{
    abstract class Transaction
    {
        public DateTime CreationDate = DateTime.Now;

        public abstract void Save();
        public abstract void Remove();
    }
}
