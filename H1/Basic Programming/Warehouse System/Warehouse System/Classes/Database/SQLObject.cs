using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse_System
{
    abstract class SQLObject
    {
        protected bool Saved = false;
        public int Id { get; set; }

        public abstract void Save();
        public abstract void Remove();
    }
}
