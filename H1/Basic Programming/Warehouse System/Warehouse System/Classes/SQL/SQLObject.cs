using System;
using System.Collections.Generic;
using System.Text;
namespace Warehouse_System.Classes.SQL
{
    abstract class SQLObject
    {
        protected bool Saved = false;
        public int Id { get; set; }


        protected void Insert(string query)
        {
            if (!Saved)
            {
                this.Id = Database.ExecuteAndGetId(query);
                this.Saved = true;
            }
        }
        protected void Delete(string query)
        {
            if (Saved)
            {
                Database.Execute(query);
                this.Saved = false;
            }
        }

        protected void Update(string query)
        {
            Database.Execute(query);
        }

    }
}
