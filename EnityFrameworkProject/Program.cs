using EnityFrameworkProject.Data;
using EnityFrameworkProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

MyDBContext contex = new MyDBContext();
Console.WriteLine("tutorial: https://www.youtube.com/watch?v=SryQxUeChMc&list=PLdo4fOcmZ0oXCPdC3fTFA3Z79-eVH3K-s");

/// <summary>Metoda dodaje "ręcznie" okreśone produkty</summary>
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

void ShowProducts()
{
    var products = from product in contex.Products
                   where product.Price > 7.00M
                   orderby product.Name
                   select product;

    foreach (Product product in products)
    {
        Console.WriteLine(product.Name);
        Console.WriteLine(product.Price);
        Console.WriteLine("--------------");
    }
}

void ShowCustomers()
{
    var customers = from customer in contex.Customers
                   orderby customer.LastName
                   select customer;

    foreach (Customer c in customers)
    {
        Console.WriteLine(c.FirstName);
        Console.WriteLine(c.LastName);
        Console.WriteLine(c.Email);
        Console.WriteLine("--------------");
    }
}

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
/// <summary>Opróżnienie tabel bazy danych</summary>
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

Product getProduct(string productName)
{
    foreach(Product product in contex.Products)
    {
        if (product.Name == productName)
            return product;
    }
    return null;
}

int getCustomerId(string customerName)
{
    foreach (Customer customer in contex.Customers)
    {
        if (customer.LastName == customerName)
            return customer.Id;
    }
    return 0;
}

int AddOrder(string productName, string customerName, int quantity, DateTime orderTime)
{
    Product product = getProduct(productName);
    int customerId = getCustomerId(customerName);
    Order order = new Order();
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
    foreach (Order order in contex.Orders)
    {
        Console.WriteLine("Numer zamówienia: " + order.Id);
        Console.WriteLine("Data: " + order.OrderPlaced.ToString());
        Console.WriteLine("Klient: " + getCustomerName(order.CustomerId));
        decimal suma = 0;
        foreach (OrderDetail detail in order.OrderDetails)
        {
            Console.WriteLine("Produkt: " + getProductById(detail.ProductId).Name);
            if (detail.OrderId == order.Id)
            {
                Console.WriteLine("Ilość: " + detail.Quantity);
            }
            decimal kwota = detail.Product.Price * detail.Quantity;
            suma += kwota;
            Console.WriteLine("Kwota: " + (kwota).ToString("C2"));
        }
        Console.WriteLine("Razem do zapłaty : " +  suma.ToString("C2"));
        Console.WriteLine("--------------");
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
    foreach (var order in orders)
    {
        Console.WriteLine("Details: " + order.OrderDetails.Count);
    }
}

IncludeOrderDetails();

ZipDataBase();
AddProducts();
AddProductsFromFile();
AddCustomersFromFile();
ShowProducts();
ShowCustomers();
int orderId = AddOrder("Grochówka", "Ceglarek", 1, DateTime.Now);
AddProductToOrder("Sandacz po zamordejsku", 1, orderId);
AddProductToOrder("Zupa pomidorowa", 1, orderId);
AddProductToOrder("Miętus po argentyńsku", 2, orderId);
orderId = AddOrder("Golonka po bawarsku", "Polańska", 2, DateTime.Now);
AddProductToOrder("Kaszanka z ziemniakami", 2, orderId);
orderId = AddOrder("Kapuśniak", "Czapiewska", 1, DateTime.Now);
AddProductToOrder("Sandacz po zamordejsku", 1, orderId);

ShowOrders();


