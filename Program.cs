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
            JTokenSample1();
        }


        private static void SerializeSample()
        {
            var transaction = Initialize.GetTransaction();
            string json = JsonConvert.SerializeObject(transaction);
            transaction = JsonConvert.DeserializeObject<object>(json);
        }


        private static void AnonymousType()
        {
            string json = GetSerializedTransaction();

            // Quando não queremos construir classes para deserializar JSON, podemos usar objetos anonimos:
            var definition = new
            {
                TransactionId = "",
                Orders = new List<object>()
            };

            var transaction = JsonConvert.DeserializeAnonymousType(json, definition);

            var primeiraOrdem = transaction.Orders.First();

            Console.WriteLine($"Transaction: {transaction.TransactionId}; Order: {primeiraOrdem}");
        }



        private static void SerializeIdentedJSON()
        {
            List<Order> orders = Initialize.InitOrders();

            string json = JsonConvert.SerializeObject(orders, Formatting.Indented);
            Console.WriteLine(json);
        }


        // Quando não queremos construir nem mesmo objetos anonimos, podemos trabalhar com JToken:
        private static void JTokenSample1()
        {
            string json = GetSerializedTransaction();

            JToken jToken = JToken.Parse(json);

            JToken primeiraOrdem = jToken["Orders"]?.First();
            string nomeClientePrimeiraOrdem = jToken["Orders"]?.First()["Customer"]["Name"]?.ToString();
            string nomeRuaEnderecoClientePrimeiraOrdem = jToken["Orders"].First()["Customer"]["Address"]["Street"]?.ToString();
            Address enderecoClientePrimeiraOrdem = jToken["Orders"]?.First()["Customer"]["Address"]?.ToObject<Address>();
        }


        private static void JTokenSample2()
        {
            string json = GetSerializedTransaction();

            // Converte a string JSON para um JToken
            JToken jToken = JToken.Parse(json);

            JToken segundaOrdem = jToken.SelectToken("Orders[1]");
            string nomeClienteSegundaOrdem = jToken.SelectToken("Orders[1].Customer.Name")?.ToString();
            string nomeRuaEnderecoClienteSegundaOrdem = jToken.SelectToken("Orders[1].Customer.Address.Street")?.ToString();
            Address enderecoClientePrimeiraOrdem = jToken.SelectToken("Orders[1].Customer.Address")?.ToObject<Address>();
        }


        private static string GetSerializedTransaction()
        {
            var transaction = Initialize.GetTransaction();
            string json = JsonConvert.SerializeObject(transaction);
            return json;
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
    }
}
