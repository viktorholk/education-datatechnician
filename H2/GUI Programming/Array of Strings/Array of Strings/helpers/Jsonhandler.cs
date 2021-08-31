using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
namespace Array_of_Strings.helpers
{
    public class Jsonhandler
    {

        public static object[] Load<T>(string file) where T : class
        {
            using (StreamReader r = new StreamReader(file))
            {
                // Read the file into a string
                string json = r.ReadToEnd();
                // Convert the string to the array of items and return it
                return JsonConvert.DeserializeObject<T[]>(json);
            };
        }
    }
}
