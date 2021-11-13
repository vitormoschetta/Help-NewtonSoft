using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Help_NewtonSoft
{
    class Program
    {
        static void Main(string[] args)
        {
            Process();
        }

        private static void Process()
        {
            List<Order> orders = Initialize.InitOrders();

            string json = JsonConvert.SerializeObject(orders); 

            orders = JsonConvert.DeserializeObject<List<Order>>(json);
            
            // Se não quiser criar classes para deserializar, e ainda trabalhar Linq C#:
            JToken jToken = JToken.Parse(json);
            JToken primeiraOrdem = jToken[0];
            // or:
            primeiraOrdem = jToken.First();
            JToken nomeCliente = jToken.First()["Customer"]["Name"];
            string nomeRuaClienteMora = jToken.First()["Customer"]["Address"]["Street"].ToString();
        }

        private static void PrintIdented()
        {
            List<Order> orders = Initialize.InitOrders();

            string json = JsonConvert.SerializeObject(orders, Formatting.Indented); 
            Console.WriteLine(json);
        }

        private static void WriterFile()
        {
            List<Order> orders = Initialize.InitOrders();
            File.WriteAllText(@"./orders.json", JsonConvert.SerializeObject(orders));
            
            using (StreamWriter file = File.CreateText(@"./orders.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, orders);
            }
        }

        private static void AnonymousType()
        {
            var definition = new { Name = "" };

            string json1 = @"{'Name':'James'}";
            var customer1 = JsonConvert.DeserializeAnonymousType(json1, definition);

            Console.WriteLine(customer1.Name);
        }
        
    }
}
