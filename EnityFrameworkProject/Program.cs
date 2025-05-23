﻿using EnityFrameworkProject.Data;
using EnityFrameworkProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
///<summary>context jest głównym eobiektem w programie</summary>
MyDBContext contex = new MyDBContext();
Console.WriteLine("tutorial: https://www.youtube.com/watch?v=SryQxUeChMc&list=PLdo4fOcmZ0oXCPdC3fTFA3Z79-eVH3K-s");

/// <summary>Metoda dodaje "ręcznie" okreśone produkty (dania restauracyjne)</summary>
void AddProducts()
{
    Product simplePizza = new Product()
    {
        Name = "Prosta pizza",
        Price = 9.99M
    };

    contex.Products.Add(simplePizza);

    Product gulasz = new Product()
    {
        Name = "Gulasz węgierski",
        Price = 25.93M
    };

    contex.Products.Add(gulasz);

    Product ros = new Product()
    {
        Name = "Rosół wielkopolski",
        Price = 12.30M
    };

    contex.Products.Add(ros);

    contex.SaveChanges();
}

void ShowMenu()
{
    var products = from product in contex.Products
                   where product.Price > 7.00M
                   orderby product.Name
                   select product;
    Console.WriteLine("Menu:");
    foreach (Product product in products)
    {
        Console.WriteLine("\t" + product.Name);
        Console.WriteLine("\t" + product.Price);
        Console.WriteLine("\t--------------");
    }
}
/// <summary>Metoda wyświetla informacje o wszystkich klientach restauracji</summary>
void ShowCustomers()
{
    var customers = from customer in contex.Customers
                   orderby customer.LastName
                   select customer;
    Console.WriteLine("Klienci restauracji: ");
    foreach (Customer c in customers)
    {
        Console.WriteLine("\t" + c.Id);
        Console.WriteLine("\t" + c.FirstName);
        Console.WriteLine("\t" + c.LastName);
        Console.WriteLine("\t" + c.Email);
        Console.WriteLine("\t--------------");
    }
}

/// <summary>Metoda dodaje produkty (dania restauracyjne) do bazy danych</summary>
void AddProductsFromFile()
{
    var stream = new FileStream("..\\..\\..\\Dane\\products.dat", FileMode.Open);
    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        string[] sep = line.Split(';');
        Product p = new Product();
        p.Name = sep[0];
        p.Price = Convert.ToDecimal(sep[1]);
        contex.Products.Add(p);
    }
    reader.Close();
    stream.Close();
    contex.SaveChanges();
}

void AddCustomersFromFile()
{
    var stream = new FileStream("..\\..\\..\\Dane\\customers.dat", FileMode.Open);
    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        string[] sep = line.Split(';');
        Customer c = new Customer();
        c.FirstName = sep[0];
        c.LastName = sep[1];
        c.Email = sep[2];
        c.Phone = sep[3];
         contex.Customers.Add(c);
    }
    reader.Close();
    stream.Close();
    contex.SaveChanges();
}
/// <summary>Metoda realizuke opróżnienie tabel bazy danych</summary>
void ZipDataBase()
{
  foreach (Product product in contex.Products)
    {
        contex.Products.Remove(product);
    }
    contex.SaveChanges(true);

  foreach (Customer c in contex.Customers)
  {
        contex.Customers.Remove(c);
  }
    foreach (Order o in contex.Orders)
        contex.Orders.Remove(o);

    contex.SaveChanges(true);

}

/// <summary>Metoda zwraca obiekt Product (danie restauracyjne) na podstawie nazwy produktu (dania)</summary>
Product getProduct(string productName)
{
    foreach(Product product in contex.Products)
    {
        if (product.Name == productName)
            return product;
    }
    return null;
}

///<summary>Metoda zwraca numer zamówienia na podstawie nazwy klienta</summary>
int getCustomerId(string customerName)
{
    foreach (Customer customer in contex.Customers)
    {
        string name = customer.FirstName + " " + customer.LastName;
        if (name == customerName)
            return customer.Id;
    }
    return 0;
}

///<summary>Metoda tworzy zamówienie i dodaje zamówienie je do tabeli zamówień</summary>
int AddOrder(string productName, string customerName, int quantity, DateTime orderTime)
{
    Product product = getProduct(productName);
    int customerId = getCustomerId(customerName);
    Customer customer = getCustomer(customerId);
    Order order = new Order();
    order.Customer = customer;
    order.CustomerId = customerId;
    order.OrderPlaced = orderTime;
    order.OrderDetails = new List<OrderDetail>();
    OrderDetail detail = new OrderDetail();
    detail.ProductId = product.Id;
    detail.Order = order;
    detail.Quantity = quantity;
    detail.Product = product;
    detail.ProductId = product.Id;
    order.OrderDetails.Add(detail);
    contex.Orders.Add(order);
    contex.SaveChanges();
    return order.Id;
}

///<summary>Metoda zwraca obiekt klienta na podstawie jego Id</summary>
Customer getCustomer(int customerId)
{
    foreach (Customer customer in contex.Customers)
        if (customer.Id == customerId)
            return customer;
    return null;
}
///<summary>Metoda dodaje produkt (danie) do zamówienia</summary
void AddProductToOrder(string productName, int quantity, int orderId)
{
    OrderDetail detail = new OrderDetail();
    detail.Product = getProduct(productName);
    detail.ProductId = detail.Product.Id;
    Order order = getOrder(orderId);
    detail.Quantity = quantity;
    detail.Order = order;
    order.OrderDetails.Add(detail);
}

Order getOrder(int orderId)
{
    foreach(Order order in contex.Orders)
        if (order.Id == orderId)
            return order;
    return null;
}

/// <summary>Metoda zwraca imię i nazwisko klienta na podstawie jego Id</summary>
/// <param>customerId jest identyfikatorem klienta</param>
/// <returns>zwraca imię i nazwisko klienta</returns>
string getCustomerName(int customerId)
{
    foreach (Customer c in contex.Customers)
    {
        if (c.Id == customerId)
            return c.FirstName + " " + c.LastName;
    }
    return "";
}


/// <summary>
/// Metoda zwraca Product na podstawie jego Id
/// </summary>
/// <param>productId jest identyfikatorem produktu</param>
/// <returns>zwraca obiekt Product</returns>
Product getProductById(int productId)
{
    foreach (Product p in contex.Products)
        if (p.Id == productId)
            return p;
    return null;
}

/// <summary>
/// Metoda wyświetla szczegółowe dnae o wszystkich zamówieniach (Orders)
/// </summary>
void ShowOrders()
{
    Console.WriteLine("Zamówienia:");
    foreach (Order order in contex.Orders)
    {
        Console.WriteLine("\tNumer zamówienia: " + order.Id);
        Console.WriteLine("\tData: " + order.OrderPlaced.ToString());
        Console.WriteLine("\tKlient: " + getCustomerName(order.CustomerId));
        decimal suma = 0;
        foreach (OrderDetail detail in order.OrderDetails)
        {
            Console.WriteLine("\t\tProdukt: " + getProductById(detail.ProductId).Name);
            if (detail.OrderId == order.Id)
            {
                Console.WriteLine("\t\tIlość: " + detail.Quantity);
            }
            decimal kwota = detail.Product.Price * detail.Quantity;
            suma += kwota;
            Console.WriteLine("\t\tKwota: " + (kwota).ToString("C2"));
        }
        Console.WriteLine("\tRazem do zapłaty : " +  suma.ToString("C2"));
        Console.WriteLine("\t--------------");
    }
}
/// <summary>
/// Metoda dołącza powiązanie pomiędzy Orders a OrderDetails
/// </summary>
void IncludeOrderDetails()
{
    var cont = contex;
    var orders = cont.Orders
       .Include(o => o.OrderDetails)
       .ToList();
    /*
    foreach (var order in orders)
    {
        Console.WriteLine("Details: " + order.OrderDetails.Count);
    }
    */
}

IncludeOrderDetails();

ZipDataBase();
AddProducts();
AddProductsFromFile();
AddCustomersFromFile();
ShowMenu();

ShowCustomers();

int orderId = AddOrder("Grochówka", "Dariusz Ceglarek", 1, DateTime.Now);
AddProductToOrder("Sandacz po zamordejsku", 1, orderId);
AddProductToOrder("Zupa pomidorowa", 1, orderId);
AddProductToOrder("Miętus po argentyńsku", 2, orderId);
orderId = AddOrder("Golonka po bawarsku", "Katarzyna Polańska", 2, DateTime.Now);
AddProductToOrder("Kaszanka z ziemniakami", 2, orderId);
orderId = AddOrder("Kapuśniak", "Ewa Czapiewska", 1, DateTime.Now);
AddProductToOrder("Sandacz po zamordejsku", 1, orderId);
orderId = AddOrder("Boczek z pieczarkami", "Jerzy Balicki", 2, DateTime.Now);
AddProductToOrder("Barszcz", 2, orderId);
AddProductToOrder("Kaszanka z ziemniakami", 2, orderId);

ShowOrders();


