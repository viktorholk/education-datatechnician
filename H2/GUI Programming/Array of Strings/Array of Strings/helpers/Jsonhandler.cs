using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
namespace Array_of_Strings.helpers
{
    public class Jsonhandler
    {
        public class Item
        {
            public string URL { get; set; }
            public string Text { get; set; }
            public string CountryCode { get; set; }
        }

        public static Item[] Load(string file)
        {
            List<Item> items;
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Item>>(json);
            };
            return items.ToArray();
        }
    }
}
