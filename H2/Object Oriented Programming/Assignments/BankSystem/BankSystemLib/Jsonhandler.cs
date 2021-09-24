using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BankSystemLib{

    public abstract class Jsonhandler {
        private static string FilePath = "UserData.json";

        public static T Read<T>() where T : class{
            // We dont want the program to fail if the files doesn't exist
            // Even tho the data will be null
            if (!File.Exists(FilePath)){
                // Write an empty user list
                using (StreamWriter sw = new StreamWriter(FilePath)){
                    sw.Write("[]");
                }
            }

            T obj =  Newtonsoft.Json.JsonConvert.DeserializeObject<T>(File.ReadAllText(FilePath), new Newtonsoft.Json.JsonSerializerSettings 
            { 
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });

            return obj;
        }

        public static void Write(string data){
            using (StreamWriter w = new StreamWriter(FilePath))
                w.Write(data);
        }


        public static void Write<T>(object obj) where T : class{
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.NullValueHandling    = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling     = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting           = Newtonsoft.Json.Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(FilePath))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj, typeof(T));
            }
        }
    }
}