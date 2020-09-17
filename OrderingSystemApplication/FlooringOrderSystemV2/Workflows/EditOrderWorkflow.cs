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
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            DateTime orderDateToUpdate;
            int orderNumberToUpdate;

            Console.Clear();
            Console.WriteLine("Update an Order");
            Console.WriteLine("--------------------------------");

            var dateFormats = new[] { "MM/dd/yyyy", "M/dd/yyyy", "MM/d/yyyy", "M/d/yyyy", "MM/dd/y", "M/dd/y", "MM/d/y", "M/d/y" };
            Console.Write("Enter a Date (MM/dd/yyyy): ");
            string userInput = Console.ReadLine();
            DateTime orderDate;
            DateTime.TryParseExact(userInput, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out orderDate);
            orderDateToUpdate = orderDate;
           
            Console.Write("Enter the order number: ");
            String orderNumInput = Console.ReadLine();
            int orderNum;
            Int32.TryParse(orderNumInput, out orderNum);
            orderNumberToUpdate = orderNum;

            // locate order object in repository
            LocateOrderResponse response = manager.LocateOrder(orderDateToUpdate, orderNumberToUpdate);

            Order orderToEdit = new Order();

            if (response.Success)
            {
                // display preview of order object user wishes to edit
                Console.WriteLine();
                Console.WriteLine("Order to Edit details:");
                Console.WriteLine("----------------------------------");
                ConsoleIO.DisplayPreview(response.Order);
                Console.WriteLine();

                // prompt user for new info... if left blank, keep original value in place
                Console.WriteLine("Please provide the following updated information: ");

                Console.Write("Customer Name: ");
                string newName = Console.ReadLine();
                if(String.IsNullOrEmpty(newName))
                {
                    orderToEdit.CustomerName = response.Order.CustomerName;
                }
                else
                {
                    orderToEdit.CustomerName = newName;
                }

                bool halt = false;
                while(!halt)
                {
                    Console.Write("State (Abbr.): ");
                    string newState = Console.ReadLine();

                    if (String.IsNullOrEmpty(newState))
                    {
                        orderToEdit.State = response.Order.State;
                        halt = true;
                    }
                    else
                    {
                        if (newState != "PA" && newState != "OH" && newState != "MI" && newState != "IN")
                        {
                            Console.WriteLine("Ordering not available outside of PA, OH, MI, and IN.");
                        }
                        else
                        {
                            orderToEdit.State = newState;
                            halt = true;
                        }                        
                    }
                }

                Console.Write("Product Type (Carpet, Laminate, Tile, or Wood): ");
                string newProductType = Console.ReadLine();
                if(String.IsNullOrEmpty(newProductType))
                {
                    orderToEdit.ProductType = response.Order.ProductType;
                }
                else
                {
                    orderToEdit.ProductType = newProductType;
                }

                bool stop = false;
                while(!stop)
                {
                    Console.Write("Area: ");
                    string newArea = Console.ReadLine();

                    if (String.IsNullOrEmpty(newArea))
                    {
                        orderToEdit.Area = response.Order.Area;
                        stop = true;
                    }
                    else
                    {
                        decimal areaInput = Convert.ToDecimal(newArea);

                        if(areaInput >= 100)
                        {
                            orderToEdit.Area = areaInput;
                            stop = true;
                        }
                        else
                        {
                            Console.WriteLine("Order must be at least 100 sq. feet.");
                        }
                    }
                }

                orderToEdit.OrderDate = response.Order.OrderDate;
                orderToEdit.OrderNumber = response.Order.OrderNumber;

                // calculate & save calculated fields
                orderToEdit = GetCalculatedFieldsFromFile.CalculateFieldsFromFile(orderToEdit);
                
                // preview updated order
                ConsoleIO.DisplayPreview(orderToEdit);

                // Confirm changes?
                Console.WriteLine();
                Console.Write("Please confirm that you would like to save these changes (Y/N): ");
                string confirmUpdate = Console.ReadLine();

                if (confirmUpdate == "Y")
                {                                      
                    UpdateOrderResponse updateResponse = manager.UpdateOrder(orderToEdit);

                    if (updateResponse.Success)
                    {
                        Console.WriteLine("Order updated!");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("An error occurred...");
                        Console.WriteLine(updateResponse.Message);
                    }

                }
                else
                {
                    Console.WriteLine("Changes not saved.");
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
