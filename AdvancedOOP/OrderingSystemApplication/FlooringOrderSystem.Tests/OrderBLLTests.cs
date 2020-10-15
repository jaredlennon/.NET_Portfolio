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
    public class OrderBLLTests
    {
        [Test]
        public void CanGenerateNextOrderNumber()
        {
            Order testOrder = new Order
            {                
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

            testOrder.OrderNumber = GetOrderNumber.NextOrderNumber(testOrder.OrderDate);

            Assert.AreEqual(testOrder.OrderNumber, 6);
        }

        [Test]
        public void CanCalculateTax()
        {
            Order testOrder = new Order
            {
                OrderDate = DateTime.Today,
                CustomerName = "Test Order",
                State = "MI",
                ProductType = "Tile",
                Area = 100.00M
            };

            OrderManager manager = OrderManagerFactory.Create();
            AddOrderResponse response = manager.AddOrder(testOrder);

            Assert.AreEqual(response.Order.TaxRate, 5.75);
            Assert.AreEqual(response.Order.Tax, 43.9875);
        }

        [Test]
        public void CanCalculateCosts()
        {
            Order testOrder = new Order
            {
                OrderDate = DateTime.Today,
                CustomerName = "Test Order",
                State = "MI",
                ProductType = "Tile",
                Area = 100.00M
            };

            OrderManager manager = OrderManagerFactory.Create();
            AddOrderResponse response = manager.AddOrder(testOrder);

            Assert.AreEqual(response.Order.CostPerSquareFoot, 3.50);
            Assert.AreEqual(response.Order.LaborCostPerSquareFoot, 4.15);
            Assert.AreEqual(response.Order.MaterialCost, 350);
            Assert.AreEqual(response.Order.LaborCost, 415);
            Assert.AreEqual(response.Order.Total, 808.9875);
        }

        [Test]
        public void CanOnlyOrderInSpecifiedStates()
        {
            Order testOrder = new Order
            {
                OrderNumber = 1,
                OrderDate = DateTime.Today,
                CustomerName = "Test Order",
                State = "TX",
                ProductType = "Tile",
                Area = 100.00M
            };

            OrderManager manager = OrderManagerFactory.Create();
            AddOrderResponse response = manager.AddOrder(testOrder);

            Assert.AreEqual(response.Order.TaxRate, 0);
        }

        [Test]
        public void CanOnlyOrderSpecifiedProducts()
        {
            Order testOrder = new Order
            {
                OrderNumber = 1,
                OrderDate = DateTime.Today,
                CustomerName = "Test Order",
                State = "OH",
                ProductType = "Marble",
                Area = 100.00M
            };

            OrderManager manager = OrderManagerFactory.Create();
            AddOrderResponse response = manager.AddOrder(testOrder);

            Assert.AreEqual(response.Order.CostPerSquareFoot, 0);
        }
    }
}
