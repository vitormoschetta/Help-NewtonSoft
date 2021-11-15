using System;
using System.Collections.Generic;
using System.Linq;

namespace Help_NewtonSoft
{
    public static class Initialize
    {
        public static object GetTransaction()
        {
            return new
            {
                TransactionId = Guid.NewGuid(),
                Orders = InitOrders()
            };
        }

        public static List<Order> InitOrders()
        {
            return new List<Order>()
            {
                new Order(1, DateTime.Now, InitCustomer().First(), InitProducts().Take(2).ToList()),
                new Order(1, DateTime.Now, InitCustomer().Last(), InitProducts().Skip(2).Take(1).ToList()),
            };
        }

        private static List<Product> InitProducts()
        {
            return new List<Product>()
            {
                new Product(1, "Leite", 4.50m, true),
                new Product(2, "Manteiga", 17.00m, false),
                new Product(3, "Biscoito", 2.70m, true),
            };
        }

        private static List<Customer> InitCustomer()
        {
            return new List<Customer>()
            {
                new Customer(1, "Vitor", new Address(1, "Rua Tucurui", 3998)),
                new Customer(2, "Cristinae", new Address(2, "Rua Geraldo Siqueira", 2680))
            };
        }

    }

    public class Address
    {
        public Address(int id, string street, int number)
        {
            Id = id;
            Street = street;
            Number = number;
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }

    public class Customer
    {
        public Customer(int id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class Product
    {
        public Product(int id, string name, decimal price, bool active)
        {
            Id = id;
            Name = name;
            Price = price;
            Active = active;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }

    public class Order
    {
        public Order(int id, DateTime date, Customer customer, List<Product> products)
        {
            Id = id;
            Date = date;
            Customer = customer;
            Products = products;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
    }
}