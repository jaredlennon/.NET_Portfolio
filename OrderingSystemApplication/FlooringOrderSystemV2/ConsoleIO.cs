using FlooringOrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystemV2
{
    public class ConsoleIO
    {
        public static void DisplayOrderDetails(List<Order> order)
        {
            foreach (Order item in order)
            {
                Console.WriteLine();
                Console.WriteLine($"{item.OrderNumber} | {item.OrderDate.ToString("MM/dd/yyyy")}");
                Console.WriteLine(item.CustomerName);
                Console.WriteLine(item.State);
                Console.WriteLine($"Product: {item.ProductType}");
                Console.WriteLine($"Materials: {item.MaterialCost:C}");
                Console.WriteLine($"Labor: {item.LaborCost:C}");
                Console.WriteLine($"Tax: {item.Tax:C}");
                Console.WriteLine($"Total: {item.Total:C}");
            }
        }

        public static void DisplayPreview(Order order)
        {
            Console.WriteLine();
            Console.WriteLine($"{order.OrderNumber} | {order.OrderDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine(order.CustomerName);
            Console.WriteLine(order.State);
            Console.WriteLine($"Product: {order.ProductType}");
            Console.WriteLine($"Materials: {order.MaterialCost:C}");
            Console.WriteLine($"Labor: {order.LaborCost:C}");
            Console.WriteLine($"Tax: {order.Tax:C}");
            Console.WriteLine($"Total: {order.Total:C}");
        }

        public static void DisplayProducts()
        {
            Console.WriteLine();
            Console.WriteLine("Available Products:");
            Console.WriteLine("-------------------");
            Console.WriteLine("Carpet");
            Console.WriteLine("Material cost per sq foot: $2.25");
            Console.WriteLine("Labor cost per sq foot: $2.10");
            Console.WriteLine("-------------------");
            Console.WriteLine("Laminate");
            Console.WriteLine("Material cost per sq foot: $1.75");
            Console.WriteLine("Labor cost per sq foot: $2.10");
            Console.WriteLine("-------------------");
            Console.WriteLine("Tile");
            Console.WriteLine("Material cost per sq foot: $3.50");
            Console.WriteLine("Labor cost per sq foot: $4.15");
            Console.WriteLine("-------------------");
            Console.WriteLine("Wood");
            Console.WriteLine("Material cost per sq foot: $5.15");
            Console.WriteLine("Labor cost per sq foot: $4.75");
            Console.WriteLine("-------------------");
        }
    }
}
