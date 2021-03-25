using System;
using System.Collections.Generic;
using System.Linq;
namespace Aflevering
{
    /// <summary>
    /// The Shelf Class.
    /// The Constructor takes in the unitStorageSize which is how much the shelf can store of products, and a description.
    /// 
    /// </summary>
    class Shelf
    {
        // Static list of all shelves
        public static List<Shelf> shelves = new List<Shelf>();
        // Class list of all the products on the shelf
        public List<Product> products = new List<Product>();

        public char Identifier;
        public string Description;
        public int UnitStorageSize;

        private char GetIdentifier() 
        { 
            // If there is mulitple shelves increment the char to the next
            if (shelves.Count > 0)
            {
                char previousIdentifier = shelves.Last().Identifier;
                return (char)(Convert.ToInt16(previousIdentifier) + 1);

            }
            // No shelves has been set yet. Return the start of the shelves with the identifier of A
            return 'A';
        }

        public Shelf(int unitStorageSize = 10, string description = "No description has been set")
        {
            this.UnitStorageSize = unitStorageSize;
            this.Description = description;
            this.Identifier = GetIdentifier();
            // Create the shelf identifier
            // The shelf identifier is a char from A-Z, each new shelf created with increment this
            // If there is no shelves set the Shelf identifier to A

            // Get the last shelf in the list and increment the char digit to the next
            shelves.Add(this);
        }

        public void AddProduct(Product product)
        {
            // Get the current items on the shelf and take the sum of all of their unit size and check if the product can be listed on the shelf
            int totalUnits = 0;
            products.ForEach(i => totalUnits += i.UnitSize);
            // Check if there is room for the product
            if (totalUnits < UnitStorageSize && (totalUnits + product.UnitSize) <= UnitStorageSize)
            {
                products.Add(product);
            }
            // There was no room, write reason
            else Console.WriteLine($"There is no room for {product.Name} on shelf {this}");

        }

        public override string ToString()
        {
            return $"{this.Identifier}, {this.Description}";
        }
    }


    /// <summary>
    /// Product Class
    /// This class takes in name and description parameters to set the Product object
    /// </summary>
    /// 
    public class Product
    {
        public static List<Product> products = new List<Product>();

        public enum Types
        {
            Electronics,
            Convenience,
            Agriculture
        }

        // The type of product, if its either in the electronics category of it its food related, for instance.
        public Types Type;
        public string Name;
        public string Description;

        // The unitsize will determine if there is room of the product on the shelf
        public int UnitSize;

        public Product(Types type, string name, string description, int unitSize = 1)
        {
            this.Type           = type;
            this.Name           = name;
            this.Description    = description;
            this.UnitSize       = unitSize;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
