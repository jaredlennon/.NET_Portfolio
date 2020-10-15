using FlooringOrderSystem.BLL;
using FlooringOrderSystem.Models;
using FlooringOrderSystem.Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.Tests
{
    [TestFixture]
    public class OrderDataTests
    {
        [Test]
        public void CanLoadOrderTestData()
        {
            OrderManager manager = OrderManagerFactory.Create();

            DisplayOrdersResponse response = manager.DisplayOrders(DateTime.Today);

            Assert.IsNotNull(response.Orders);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void CanLocateOrder()
        {
            OrderManager manager = OrderManagerFactory.Create();

            LocateOrderResponse response = manager.LocateOrder(DateTime.Today, 1);

            Assert.IsNotNull(response.Order);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void CanAddOrder()
        {
            Order testOrder = new Order
            {
                OrderNumber = 1,
                OrderDate = DateTime.Today,
                CustomerName = "Test Order",
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
            };

            OrderManager manager = OrderManagerFactory.Create();

            AddOrderResponse response = manager.AddOrder(testOrder);

            Assert.IsNotNull(response.Order);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Order.CustomerName, "Test Order");
        }

        [Test]
        public void CanRemoveOrder()
        {
            Order testOrderToRemove = new Order
            {
                OrderNumber = 1,
                OrderDate = DateTime.Today,
                CustomerName = "Test Order",
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
            };

            OrderManager manager = OrderManagerFactory.Create();

            RemoveOrderResponse response = manager.RemoveOrder(testOrderToRemove);

            Assert.IsNotNull(response.Order);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void CanUpdateOrder()
        {
            Order originalOrder = new Order
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
            };

            Order testOrderToUpdate = new Order
            {
                OrderNumber = 1,
                OrderDate = DateTime.Today,
                CustomerName = "James Test",
                State = "IN",
                ProductType = "Wood",
                Area = 100.00M,
                TaxRate = 6.00M,
                CostPerSquareFoot = 5.15M,
                LaborCostPerSquareFoot = 4.75M,
                MaterialCost = 515.00M,
                LaborCost = 475.00M,
                Tax = 59.4M,
                Total = 1049.40M
            };

            OrderManager manager = OrderManagerFactory.Create();

            UpdateOrderResponse response = manager.UpdateOrder(testOrderToUpdate);

            Assert.IsNotNull(response.Orders);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Orders.Count, 4);
            Assert.AreNotEqual(originalOrder.CustomerName, testOrderToUpdate.CustomerName);
        }
    }
}
