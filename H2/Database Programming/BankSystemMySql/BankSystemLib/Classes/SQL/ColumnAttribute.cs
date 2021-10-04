using System;


namespace BankSystemLib {


    [System.AttributeUsage(System.AttributeTargets.Property)]  
    public class ColumnAttribute : System.Attribute  
    {  
        public string ColumnName;  
    
        public ColumnAttribute(string columnName)  
        {  
            this.ColumnName = columnName;
        }  
    }  
}