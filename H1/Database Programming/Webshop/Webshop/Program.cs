using System;
using System.Collections.Generic;
using TECHCOOL;
namespace Webshop
{
    class Program
    {
        static void PrintResults(Result result)
        {

            foreach (var key in result[0].Keys)
            {
                if (key == "id")
                    Console.Write("{0,-8}", key.ToUpper());
                else Console.Write("{0,-15}", key.ToUpper());

            }
            Console.WriteLine();


            foreach (var dict in result)
            {
                foreach (KeyValuePair<string, string> row in dict)
                {
                    if (row.Key == "id")
                        Console.Write("{0,-8}", row.Value);
                    else Console.Write("{0,-15}", row.Value);
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            SQLet.ConnectSQLite("webshop.db");

            Console.Write("Indtast kundens fornavn: ");
            string firstName = Console.ReadLine();

            Console.Write("Indtast kundens efternavn: ");
            string lastName = Console.ReadLine();

            Console.Write("Indtast kundens postnummer: ");
            string zipCode = Console.ReadLine();

            string sql = $"INSERT INTO customer (firstName, lastName, zipcode) VALUES ('{firstName}', '{lastName}', {zipCode})";

            SQLet.Execute(sql);

            PrintResults(SQLet.GetResult("SELECT * FROM customer"));
        }
    }
}
