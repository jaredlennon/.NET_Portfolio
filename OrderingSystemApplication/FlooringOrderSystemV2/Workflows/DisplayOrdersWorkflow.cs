using FlooringOrderSystem.BLL;
using FlooringOrderSystem.Models;
using FlooringOrderSystem.Models.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystemV2.Workflows
{
    public class DisplayOrdersWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Display Orders");
            Console.WriteLine("--------------------");

            var dateFormats = new[] { "MM/dd/yyyy", "M/dd/yyyy", "MM/d/yyyy", "M/d/yyyy", "MM/dd/y", "M/dd/y", "MM/d/y", "M/d/y" };
            Console.Write("Enter a Date (MM/dd/yyyy): ");
            string userInput = Console.ReadLine();
            DateTime orderDate;
            DateTime.TryParseExact(userInput, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out orderDate);

            DisplayOrdersResponse response = manager.DisplayOrders(orderDate);

            if (response.Success)
            {
                ConsoleIO.DisplayOrderDetails(response.Orders);         
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("An error occured: ");
                Console.WriteLine(response.Message);

            }
            Console.WriteLine();
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }
    }
}
