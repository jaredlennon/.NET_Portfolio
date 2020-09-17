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
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            DateTime orderDateToRemove;
            int orderNumberToRemove;

            Console.Clear();
            Console.WriteLine("Remove an Order");
            Console.WriteLine("--------------------------------");

            // get date, order# from user
            var dateFormats = new[] { "MM/dd/yyyy", "M/dd/yyyy", "MM/d/yyyy", "M/d/yyyy", "MM/dd/y", "M/dd/y", "MM/d/y", "M/d/y" };
            Console.Write("Enter a Date (MM/dd/yyyy): ");
            string userInput = Console.ReadLine();
            DateTime orderDate;
            DateTime.TryParseExact(userInput, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out orderDate);
            orderDateToRemove = orderDate;

            Console.Write("Enter the order number: ");
            String orderNumInput = Console.ReadLine();
            int orderNum;
            Int32.TryParse(orderNumInput, out orderNum);
            orderNumberToRemove = orderNum;

            // locate order object in repository
            LocateOrderResponse response = manager.LocateOrder(orderDateToRemove, orderNumberToRemove);
           
            if(response.Success)
            {
                // preview the order object
                Console.WriteLine();
                Console.WriteLine("Order to remove details:");
                Console.WriteLine("----------------------------------");
                ConsoleIO.DisplayPreview(response.Order);

                // confirm removal with user
                Console.WriteLine();
                Console.Write("Would you like to remove this order? (Y/N): ");
                string confirmDelete = Console.ReadLine();
                Console.WriteLine();

                if (confirmDelete == "Y")
                {
                    // pass the located order object above to our Remove method
                    RemoveOrderResponse removeResponse = manager.RemoveOrder(response.Order);

                    if (removeResponse.Success)
                    {
                        Console.WriteLine("Order removed!");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("An error occured: ");
                        Console.WriteLine(removeResponse.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Order was not removed.");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("An error occurred...");
                Console.WriteLine(response.Message);
            }
            
            Console.WriteLine();
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();

        }
    }
}
