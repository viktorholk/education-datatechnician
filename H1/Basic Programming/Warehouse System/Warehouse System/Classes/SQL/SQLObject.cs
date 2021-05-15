using System;
using System.Collections.Generic;
using System.Text;
namespace Warehouse_System.Classes.SQL
{
    /// <summary>
    /// SQLObject abstract class
    /// This is the parent class of all of the SQLobjects that we are going to handle
    /// This for instance is the Product, ProductCategory and Shelf classes
    /// </summary>
    abstract class SQLObject
    {
        /// <summary>
        /// Boolean whether or not the instanse has been saved in the database yet
        /// </summary>
        protected bool Saved = false;
        /// <summary>
        /// We need to know the Id of the SQLObjects when we are handling them
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Insert the object into the database and set the id of the new object Id
        /// </summary>
        /// <param name="query">Database query</param>
        protected void Insert(string query)
        {
            if (!Saved)
            {
                this.Id = Database.ExecuteAndGetId(query);
                this.Saved = true;
            }
        }
        /// <summary>
        /// Deletes the object from the db and sets saved to false
        /// </summary>
        /// <param name="query">Database query</param>
        protected void Delete(string query)
        {
            if (Saved)
            {
                Database.Execute(query);
                this.Saved = false;
            }
        }
        /// <summary>
        /// Update the object
        /// </summary>
        /// <param name="query">Database query</param>
        protected void Update(string query)
        {
            Database.Execute(query);
        }

    }
}
