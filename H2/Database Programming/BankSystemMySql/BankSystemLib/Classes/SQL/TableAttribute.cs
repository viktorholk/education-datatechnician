using System;


namespace BankSystemLib {


    [System.AttributeUsage(System.AttributeTargets.Class)]  
    public class TableAttribute : System.Attribute  
    {  
        public string TableName;  
    
        public TableAttribute(string tableName)  
        {  
            this.TableName = tableName;
        }  
    }  
}