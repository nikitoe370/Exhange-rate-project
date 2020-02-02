using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    public class ExchangeRate
    {
        [JsonProperty(PropertyName = "rates", Order = 1)]
        public Rate Rates { get; set; }

        [JsonProperty(PropertyName = "base", Order = 2)]
        public string Base { get; set; }

        [JsonProperty(PropertyName = "date", Order = 3)]
        public DateTime Date { get; set; }
    }
    public class Rate
    {
        [JsonExtensionData]
        public Dictionary<string, JToken> Fields { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string inputcurrency = "BGN";
            Console.WriteLine("Input base currency: ");
            inputcurrency = Console.ReadLine();

            string bodi = String.Format("https://api.exchangeratesapi.io/latest?base=" + inputcurrency);
            WebRequest requestData = WebRequest.Create(bodi);
            requestData.Method = "GET";
            HttpWebResponse responseObject = null;
            responseObject = (HttpWebResponse)requestData.GetResponse();



            //Console.WriteLine(responseObject);

            string something = null;


            using (Stream stream = responseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                something = sr.ReadToEnd();
                sr.Close();
                //Console.WriteLine(something);







                var currencies = JsonConvert.DeserializeObject<ExchangeRate>(something);

                //Console.WriteLine(currencies.Rates);
                foreach (KeyValuePair<string, JToken> item in currencies.Rates.Fields)
                {
                    if (item.Key != inputcurrency)
                    { Console.WriteLine(item.Key + " " + item.Value.ToString()); }

                    //if (item.Key == "USD")
                    //{
                    //    Console.WriteLine(item.Key + " " + item.Value.ToString());
                    //}
                }






                /*
                foreach(string curr in currencies.rates) {
                    
                   

                        Console.WriteLine(curr);
                   
                } */
            };


        }
    }
}
