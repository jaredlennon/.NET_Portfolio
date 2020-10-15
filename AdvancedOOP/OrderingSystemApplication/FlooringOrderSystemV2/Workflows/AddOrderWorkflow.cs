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
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Order orderToAdd = new Order();

            // get new order info from user
            Console.Clear();
            Console.WriteLine("Add an Order");
            Console.WriteLine("--------------------");
            Console.WriteLine("Please provide the following information: ");

            var dateFormats = new[] { "MM/dd/yyyy", "M/dd/yyyy", "MM/d/yyyy", "M/d/yyyy", "MM/dd/y", "M/dd/y", "MM/d/y", "M/d/y" };

            bool stop1 = false;
            while(!stop1)
            {
                Console.Write("Order Date (MM/dd/yyyy): ");
                string userInput = Console.ReadLine();
                DateTime orderDate;

                bool validDate = DateTime.TryParseExact(userInput, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out orderDate);

                if(validDate)
                {
                    if (orderDate > DateTime.Today.Date)
                    {
                        orderToAdd.OrderDate = orderDate;
                        stop1 = true;
                    }
                    else
                    {
                        Console.WriteLine("Order date must be in the future.");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid date format entered.");
                }

            }

            bool stoprunning = false;
            while(!stoprunning)
            {
                Console.Write("Customer Name: ");
                string nameInput = Convert.ToString(Console.ReadLine());

                if (String.IsNullOrEmpty(nameInput))
                {
                    Console.WriteLine("Name field must not be blank. Please provide a name.");
                }
                else
                {
                    stoprunning = true;
                    orderToAdd.CustomerName = nameInput;
                }
            }
                   
            bool halt = false;
            while(!halt)
            {
                Console.Write("State (Abbr.): ");
                string stateInput = Convert.ToString(Console.ReadLine().ToUpper());
                
                if(stateInput != "PA" && stateInput != "OH" && stateInput != "MI" && stateInput != "IN")
                {
                    Console.WriteLine("Ordering not available outside of PA, OH, MI, and IN.");
                }
                else
                {
                    orderToAdd.State = stateInput;
                    halt = true;
                }
            }

            ConsoleIO.DisplayProducts();

            bool discontinue = false;
            while(!discontinue)
            {
                Console.Write("Product selection: ");
                string productInput = Convert.ToString(Console.ReadLine());

                if (productInput != "Carpet" && productInput != "Laminate" && productInput != "Tile" && productInput != "Wood")
                {
                    Console.WriteLine("Orders restricted to the following products: Carpet, Laminate, Tile, Wood.");
                }
                else
                {
                    orderToAdd.ProductType = productInput;
                    discontinue = true;
                }
            }

            bool stop = false;
            while (!stop)
            {
                Console.Write("Area desired (sq. feet): ");
                string areaUserInput = Console.ReadLine();

                if(String.IsNullOrEmpty(areaUserInput))
                {
                    Console.WriteLine("Must provide an area amount for the order.");
                }
                else
                {
                    decimal areaInput = Convert.ToDecimal(areaUserInput);
                                        
                    if (areaInput >= 100)
                    {
                        orderToAdd.Area = areaInput;
                        stop = true;
                    }
                    else
                    {
                        Console.WriteLine("Order must be at least 100 sq. feet.");
                    }
                }

            }

            // generate next order #
            orderToAdd.OrderNumber = GetOrderNumber.NextOrderNumber(orderToAdd.OrderDate);

            // calculate & save calculated fields
            orderToAdd = GetCalculatedFieldsFromFile.CalculateFieldsFromFile(orderToAdd);

            // display preview of new order
            Console.WriteLine("Pending Order Details");
            Console.WriteLine("-------------------------");
            ConsoleIO.DisplayPreview(orderToAdd);

            // confirm - would you like to place the order?
            Console.WriteLine();
            Console.Write("Would you like to place the order? (Y/N): ");
            string confirmOrder = Console.ReadLine();

            if(confirmOrder == "Y" )
            {
                AddOrderResponse response = manager.AddOrder(orderToAdd);

                if (response.Success)
                {
                    Console.WriteLine("Order placed!");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("An error occured: ");
                    Console.WriteLine(response.Message);
                }
            }
            else
            {
                Console.WriteLine("Order was not placed.");
            }

            Console.WriteLine();
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }
    }
}
