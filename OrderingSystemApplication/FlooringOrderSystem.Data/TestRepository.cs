using FlooringOrderSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderSystem.Models;
using System.Globalization;

namespace FlooringOrderSystem.Data
{
    public class TestRepository : IOrderRepository
    {
        public static List<Order> allOrdersList = new List<Order>
        {
            new Order
            {
                OrderNumber = 1,
                OrderDate = DateTime.Today,
                CustomerName = "Jared Lennon",
                State = "MI",
                ProductType = "Tile",
                Area = 100.00M,
                TaxRate = 5.75M,
                CostPerSquareFoot = 3.50M,
                LaborCostPerSquareFoot = 4.15M,
                MaterialCost = 350.00M,
                LaborCost = 415.00M,
                Tax = 43.9875M,
                Total = 808.9875M
            },
            new Order
            {
                OrderNumber = 2,
                OrderDate = DateTime.Today,
                CustomerName = "Michael Jordan",
                State = "OH",
                ProductType = "Wood",
                Area = 200.00M,
                TaxRate = 6.25M,
                CostPerSquareFoot = 5.15M,
                LaborCostPerSquareFoot = 4.75M,
                MaterialCost = 1030.00M,
                LaborCost = 950.00M,
                Tax = 123.75M,
                Total = 2103.75M
            },
            new Order
            {
                OrderNumber = 3,
                OrderDate = DateTime.Today,
                CustomerName = "George Washington",
                State = "IN",
                ProductType = "Carpet",
                Area = 150.00M,
                TaxRate = 6.00M,
                CostPerSquareFoot = 2.25M,
                LaborCostPerSquareFoot = 2.10M,
                MaterialCost = 337.50M,
                LaborCost = 315.00M,
                Tax = 39.15M,
                Total = 691.65M
            }
        };

        public List<Order> LoadOrders(DateTime OrderDate)
        {
            List<Order> ListToPrint = new List<Order>();

            foreach (Order o in allOrdersList)
            {
                if (OrderDate == o.OrderDate.Date)
                {
                    ListToPrint.Add(o);                    
                }                           
            }

            return ListToPrint;
        }
                        
        public Order SaveOrders(Order order)
        {
            allOrdersList.Add(order);
            return order;
        }

        public Order LocateOrder(DateTime orderDate, int orderNum)
        {
            Order locatedOrder = new Order();

            foreach (Order o in allOrdersList)
            {
                if (orderDate == o.OrderDate && orderNum == o.OrderNumber)
                {
                    locatedOrder = o;
                    break;
                }
                else
                {
                    locatedOrder = null;
                }
            }

            return locatedOrder;
        }

        public List<Order> RemoveOrder(Order order)
        {
            List<Order> newListForRemoval = new List<Order>();

            foreach(Order o in allOrdersList)
            {
                // if the object in list does not match, copy the current lists' object into new list
                if (o.OrderDate == order.OrderDate && o.OrderNumber != order.OrderNumber)
                {
                    Order orderToNewList = new Order();
                    orderToNewList.OrderDate = o.OrderDate;
                    orderToNewList.CustomerName = o.CustomerName;
                    orderToNewList.OrderNumber = o.OrderNumber;
                    orderToNewList.ProductType = o.ProductType;
                    orderToNewList.State = o.State;
                    orderToNewList.Area = o.Area;
                    orderToNewList.MaterialCost = o.MaterialCost;
                    orderToNewList.LaborCost = o.LaborCost;
                    orderToNewList.LaborCostPerSquareFoot = o.LaborCostPerSquareFoot;
                    orderToNewList.CostPerSquareFoot = o.CostPerSquareFoot;
                    orderToNewList.Tax = o.Tax;
                    orderToNewList.Total = o.Total;
                    newListForRemoval.Add(orderToNewList);
                }

                if(o.OrderDate != order.OrderDate)
                {
                    Order orderToNewList = new Order();
                    orderToNewList.OrderDate = o.OrderDate;
                    orderToNewList.CustomerName = o.CustomerName;
                    orderToNewList.OrderNumber = o.OrderNumber;
                    orderToNewList.ProductType = o.ProductType;
                    orderToNewList.State = o.State;
                    orderToNewList.Area = o.Area;
                    orderToNewList.MaterialCost = o.MaterialCost;
                    orderToNewList.LaborCost = o.LaborCost;
                    orderToNewList.LaborCostPerSquareFoot = o.LaborCostPerSquareFoot;
                    orderToNewList.CostPerSquareFoot = o.CostPerSquareFoot;
                    orderToNewList.Tax = o.Tax;
                    orderToNewList.Total = o.Total;
                    newListForRemoval.Add(orderToNewList);
                }
            }

            allOrdersList = newListForRemoval;
            return allOrdersList;
        }

        public List<Order> UpdateOrder (Order order)
        {
            List<Order> newUpdatedList = new List<Order>();
            Order updatedOrder = order;

            foreach(Order o in allOrdersList)
            {
                if (o.OrderDate.Date == order.OrderDate.Date && o.OrderNumber == order.OrderNumber)
                {
                    o.CustomerName = updatedOrder.CustomerName;
                    o.State = updatedOrder.State;
                    o.ProductType = updatedOrder.ProductType;
                    o.Area = updatedOrder.Area;

                    o.MaterialCost = updatedOrder.MaterialCost;
                    o.LaborCost = updatedOrder.LaborCost;
                    o.LaborCostPerSquareFoot = updatedOrder.LaborCostPerSquareFoot;
                    o.CostPerSquareFoot = updatedOrder.CostPerSquareFoot;
                    o.Tax = updatedOrder.Tax;
                    o.Total = updatedOrder.Total;

                    // these cannot be changed
                    o.OrderDate = o.OrderDate;
                    o.OrderNumber = o.OrderNumber;
                    newUpdatedList.Add(updatedOrder);
                }
                else
                {
                    Order orderToNewList = new Order();
                    orderToNewList.OrderNumber = o.OrderNumber;
                    orderToNewList.OrderDate = o.OrderDate;
                    orderToNewList.CustomerName = o.CustomerName;
                    orderToNewList.Area = o.Area;
                    orderToNewList.ProductType = o.ProductType;
                    orderToNewList.State = o.State;
                    orderToNewList.MaterialCost = o.MaterialCost;
                    orderToNewList.LaborCost = o.LaborCost;
                    orderToNewList.LaborCostPerSquareFoot = o.LaborCostPerSquareFoot;
                    orderToNewList.CostPerSquareFoot = o.CostPerSquareFoot;
                    orderToNewList.Tax = o.Tax;
                    orderToNewList.Total = o.Total;
                    newUpdatedList.Add(orderToNewList);
                }

                allOrdersList = newUpdatedList;
                
            }
                return allOrdersList;
        }

    }
}
