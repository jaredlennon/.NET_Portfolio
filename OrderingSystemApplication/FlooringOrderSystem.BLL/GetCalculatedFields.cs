using FlooringOrderSystem.Data;
using FlooringOrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.BLL
{
    public class GetCalculatedFieldsFromFile
    {
        public static Order CalculateFieldsFromFile (Order order)
        {
            order.TaxRate = GetTaxesFromFile(order.State);
            order.CostPerSquareFoot = GetCostPerSqFootFromFile(order.ProductType);
            order.LaborCostPerSquareFoot = GetLaborCostsPerSqFootFromFile(order.ProductType);

            order.MaterialCost = order.Area * order.CostPerSquareFoot;
            order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
            order.Tax = ((order.MaterialCost + order.LaborCost) * (order.TaxRate / 100));
            order.Total = (order.MaterialCost + order.LaborCost + order.Tax);

            return order;
        }

        public static decimal GetTaxesFromFile(string state)
        {
            decimal taxRate;
            FileRepository prodRepo = new FileRepository();

            taxRate = prodRepo.LoadTaxRate(state);

            return taxRate;
        }

        public static decimal GetCostPerSqFootFromFile (string type)
        {
            decimal costPerSqFoot;
            FileRepository prodRepo = new FileRepository();

            costPerSqFoot = prodRepo.LoadCost(type);

            return costPerSqFoot;
        }

        public static decimal GetLaborCostsPerSqFootFromFile (string type)
        {
            decimal laborCostPerSqFoot;
            FileRepository prodRepo = new FileRepository();

            laborCostPerSqFoot = prodRepo.LoadLaborCost(type);

            return laborCostPerSqFoot;
        }

    }
}
