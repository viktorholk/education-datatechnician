using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
namespace Array_of_Strings.helpers
{
    public class Jsonhandler
    {
        // The class from which the Load method will create the objects
        public class Item
        {
            public string URL { get; set; }
            public string Text { get; set; }
            public string CountryCode { get; set; }
        }

        public static Item[] Load(string file)
        {
            using (StreamReader r = new StreamReader(file))
            {
                // Read the file into a string
                string json = r.ReadToEnd();
                // Convert the string to the array of items and return it
                return JsonConvert.DeserializeObject<Item[]>(json);
            };
        }
    }
}
