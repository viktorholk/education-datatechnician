using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCodeExersises
{
    public class Employee
    {
        public int Age;
        public int YearsEmployed;
        public bool IsRetired;

        public Employee(int _age, int _yearsEmployed, bool _isRetired)
        {
            this.Age = _age;
            this.YearsEmployed = _yearsEmployed;
            this.IsRetired = _isRetired;
        }
    }
}
