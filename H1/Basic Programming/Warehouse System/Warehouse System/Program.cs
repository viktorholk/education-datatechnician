using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Warehouse_System.Classes;
using Warehouse_System.Classes.Warehouse;
using Warehouse_System.Classes.Application;
using System.Linq;
namespace Warehouse_System
{
    /// <summary>
    /// Warehouse System
    /// A system that allows you to add, edit and remove shelf and product objects in a SQLite database 
    /// </summary>
    class Program
    {

        /// <summary>
        /// Main method
        /// We make an instance of the application class which will start the application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Application application = new Application("Warehouse System");
            application.Run();
        }
    }
}
