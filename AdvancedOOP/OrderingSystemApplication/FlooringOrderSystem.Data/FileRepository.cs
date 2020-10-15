using FlooringOrderSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringOrderSystem.Models;
using System.IO;
using System.Globalization;

namespace FlooringOrderSystem.Data
{
    public class FileRepository : IOrderRepository
    {
        DateTime today = DateTime.Today;
        string orderDataPath = @".Orders_" + DateTime.Now.ToString("MMddyyyy") + ".txt";
        string[] orderData = new string[]
        {
            "OrderNumber,OrderDate,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total",
            "1,09/01/2020,Jared Lennon,OH,6.25,Wood,100.00,5.15,4.75,515.00,475.00,61.88,1051.88",
            "2,09/01/2020,John Smith,PA,6.75,Tile,100.00,3.50,4.15,350.00,415.00,51.64,816.64",
            "3,09/01/2020,Terry Crew,MI,5.75,Carpet,100.00,2.25,2.10,225.00,210.00,25.01,460.01"
        };

        string taxDataPath = @".Taxes.txt";
        string[] taxData = new string[]
        {
            "StateAbbreviation,StateName,TaxRate",
            "OH,Ohio,6.25",
            "PA,Pennsylvania,6.75",
            "MI,Michigan,5.75",
            "IN,Indiana,6.00"
        };

        string productsDataPath = @".Products.txt";
        string[] productData = new string[]
        {
            "ProductType,CostPerSquareFoot,LaborCostPerSquareFoot",
            "Carpet,2.25,2.10",
            "Laminate,1.75,2.10",
            "Tile,3.50,4.15",
            "Wood,5.15,4.75"
        };

        private List<Order> GetAllFromFile()
        {
            if (!File.Exists(orderDataPath))
            {
                File.Create(orderDataPath).Close();
                File.WriteAllLines(orderDataPath, orderData);
            }

            string[] rows = File.ReadAllLines(orderDataPath);
            List<Order> allOrdersList = new List<Order>();
            
            for (int i = 1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');
                Order _order = new Order();
                _order.OrderNumber = Convert.ToInt32(columns[0]);
                _order.OrderDate = DateTime.Parse(columns[1]);
                _order.CustomerName = columns[2];
                _order.State = columns[3];
                _order.TaxRate = Convert.ToDecimal(columns[4]);
                _order.ProductType = Convert.ToString(columns[5]);
                _order.Area = Convert.ToDecimal(columns[6]);
                _order.CostPerSquareFoot = Convert.ToDecimal(columns[7]);
                _order.LaborCostPerSquareFoot = Convert.ToDecimal(columns[8]);
                _order.MaterialCost = Convert.ToDecimal(columns[9]);
                _order.LaborCost = Convert.ToDecimal(columns[10]);
                _order.Tax = Convert.ToDecimal(columns[11]);
                _order.Total = Convert.ToDecimal(columns[12]);

                allOrdersList.Add(_order);
            }
            return allOrdersList;
        }

        public List<Order> LoadOrders(DateTime OrderDate)
        {
            List<Order> allOrders = GetAllFromFile();
            List<Order> ListToPrint = new List<Order>();

            foreach (Order o in allOrders)
            {
                if (OrderDate == o.OrderDate.Date)
                {
                    ListToPrint.Add(o);
                }
            }

            return ListToPrint;
        }

        public List<Tax> GetTaxesFromFile()
        {
            if (!File.Exists(taxDataPath))
            {
                File.Create(taxDataPath).Close();
                File.WriteAllLines(taxDataPath, taxData);
            }

            string[] rows = File.ReadAllLines(taxDataPath);
            List<Tax> allTaxList = new List<Tax>();

            for (int i=1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');
                Tax _tax = new Tax();
                _tax.StateAbbreviation = columns[0];
                _tax.StateName = columns[1];
                _tax.TaxRate = Convert.ToDecimal(columns[2]);

                allTaxList.Add(_tax);
            }
            return allTaxList;
        }

        public decimal LoadTaxRate(string state)
        {
            List<Tax> allTaxes = GetTaxesFromFile();
            decimal taxToReturn = 0;

            foreach(Tax t in allTaxes)
            {
                if(t.StateAbbreviation == state)
                {
                    taxToReturn = t.TaxRate;
                    break;
                }
            }

            return taxToReturn;
        }

        public List<Product> GetProductsFromFile()
        {
            if(!File.Exists(productsDataPath))
            {
                File.Create(productsDataPath).Close();
                File.WriteAllLines(productsDataPath, productData);
            }

            string[] rows = File.ReadAllLines(productsDataPath);
            List<Product> allProductsList = new List<Product>();

            for (int i =1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');
                Product _product = new Product();
                _product.ProductType = columns[0];
                _product.CostPerSquareFoot = Convert.ToDecimal(columns[1]);
                _product.LaborCostPerSquareFoot = Convert.ToDecimal(columns[2]);

                allProductsList.Add(_product);
            }
            return allProductsList;
        }

        public decimal LoadCost(string type)
        {
            List <Product> allProducts = GetProductsFromFile();
            decimal costToReturn = 0;

            foreach(Product p in allProducts)
            {
                if(p.ProductType == type)
                {
                    costToReturn = p.CostPerSquareFoot;
                    break;
                }
            }

            return costToReturn;
        }

        public decimal LoadLaborCost(string type)
        {
            List<Product> allProducts = GetProductsFromFile();
            decimal laborCostToReturn = 0;

            foreach (Product p in allProducts)
            {
                if (p.ProductType == type)
                {
                    laborCostToReturn = p.LaborCostPerSquareFoot;
                    break;
                }
            }

            return laborCostToReturn;
        }
        
        public Order LocateOrder(DateTime OrderDate, int OrderNum)
        {
            List<Order> orders = GetAllFromFile();
            return orders.Where(x => x.OrderNumber == OrderNum && x.OrderDate == OrderDate).FirstOrDefault();

        }

        public List<Order> RemoveOrder(Order order)
        {
            List<Order> allOrdersList = GetAllFromFile();
            List<Order> newListForRemoval = new List<Order>();

            foreach (Order o in allOrdersList)
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
                if (o.OrderDate != order.OrderDate)
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
            SaveToFile(allOrdersList);
            return allOrdersList;
        }

        public void SaveToFile(List<Order> orders)
        {
            string header = "OrderNumber,OrderDate,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";
            List<string> fileData = new List<string>();
            fileData.Add(header);
            foreach(Order order in orders)
            {
                fileData.Add(($"{order.OrderNumber},{order.OrderDate},{order.CustomerName},{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}"));
            }
            File.WriteAllLines(orderDataPath, fileData);
        }


        public Order SaveOrders(Order order)
        {
            /*
            List<Order> orders = GetAllFromFile();
            Order oldOrder = orders.Where(x => x.OrderNumber == order.OrderNumber).FirstOrDefault();
            SaveToFile(orders);
            return order;
            */

            List<Order> listBeforeAddition = GetAllFromFile();
            listBeforeAddition.Add(order);

            List<Order> updatedOrderList = listBeforeAddition;
            SaveToFile(updatedOrderList);

            return order;
        }

        public List<Order> UpdateOrder(Order order)
        {
            List<Order> allOrdersList = GetAllFromFile();
            List<Order> newUpdatedList = new List<Order>();
            Order updatedOrder = order;

            foreach (Order o in allOrdersList)
            {
                if (o.OrderDate == order.OrderDate && o.OrderNumber == order.OrderNumber)
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
            SaveToFile(allOrdersList);
            return allOrdersList;
        }
    }
}
