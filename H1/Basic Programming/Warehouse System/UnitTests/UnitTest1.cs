using System;
using Xunit;
using Warehouse_System.Classes.Application;
using System.Collections.Generic;

namespace UnitTests
{
    class ApplicationContextUnitTestClass : ApplicationContext
    {
        public Dictionary<string, int> GetPropertiesLengthPublic<T>(List<T> list)
        {
            return GetPropertiesLength(list);
        }
    }
    class ObjectClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ObjectClass(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    
    public class UnitTest1
    {
        [Fact]
        public void GetPropertiesLengthUnitTest()
        {
            ApplicationContextUnitTestClass applicationContextUnitTestClass = new ApplicationContextUnitTestClass();

            List<ObjectClass> objects = new List<ObjectClass>()
            {
                new ObjectClass(1,"shrt nam"),
                new ObjectClass(22,"short name"),
                new ObjectClass(333,"shorter name"),
                new ObjectClass(4444,"shortest name"),
                new ObjectClass(55555,"shortester name"),
                new ObjectClass(666666,"shortesterer name"),
                new ObjectClass(7777777,"long name"),
                new ObjectClass(123,"boing boing")
            };
            Dictionary<string, int> propertiesLength = applicationContextUnitTestClass.GetPropertiesLengthPublic(objects);

            Assert.Equal(17, propertiesLength["Name"]);
            Assert.Equal(7, propertiesLength["Id"]);
            
        }

    }
}
