using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse_System
{
    abstract class Transaction : SQLObject
    {
        public DateTime CreationDate = DateTime.Now;

    }
}
