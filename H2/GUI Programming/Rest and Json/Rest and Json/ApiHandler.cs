using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Rest_and_Json
{

    public class Person
    {
        public bool Active { get; set; }
        public string Age { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }
    public class ApiHandler
    {
        private static readonly string GetListOfPersonsURL = @"http://restpublic.junoeuro.dk/service1.svc/getListOfPersons/";
        private static readonly string AddPersonURL = @"http://restpublic.junoeuro.dk/service1.svc/AddPerson";


        private static HttpClient client = new HttpClient();

        public static Person[] GetPeople()
        {
            HttpResponseMessage response =  client.GetAsync(GetListOfPersonsURL).Result;

            if (response.IsSuccessStatusCode)
            {
                string data =  response.Content.ReadAsStringAsync().Result;
                // Deserialize the data to the person[] array and return it
                return JsonConvert.DeserializeObject<Person[]>(data);
            }
            return null;
        }

        public static void Addperson(Person person)
        {
            if (person.URL.Substring(person.URL.Length -1) == "/")
            {
                person.URL = person.URL[0..^1];
            }

            string url = $@"{AddPersonURL}/{person.Name}/{person.Description}/{person.URL}/{person.Active}/{person.Age}";
            
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Successfully added a new person");
            }
        }

    }
}
