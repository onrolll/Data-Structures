using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;


public class Program
{
    private static IFirstLastList<Product> products = FirstLastListFactory.Create<Product>();

    private int addCounter = 0;

    private void AddProducts(int count)
    {
        for (int i = 0; i < count; i++)
        {
            ++addCounter;
                products.Add(
                new Product(addCounter % 1000, "Product" + addCounter));
        }
    }

    class Product : IComparable<Product>
    {
        public string Title { get; set; }
        public decimal Price { get; set; }

        public Product(decimal price, string title)
        {
            this.Price = price;
            this.Title = title;
        }

        public int CompareTo(Product other)
        {
            return this.Price.CompareTo(other.Price);
        }
    }


    static void Main(string[] args)
    {
        
        //// Arrange
        //AddProducts(12000);

        //// Act
        //while (products.Count > 0)
        //{
        //    AddProducts(1);
        //    var first = this.products.First(1).FirstOrDefault();
        //    this.products.RemoveAll(first);
        //}
        //Console.WriteLine();
    }
}
