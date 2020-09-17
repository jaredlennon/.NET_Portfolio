using FlooringOrderSystem.Data;
using FlooringOrderSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.BLL
{
    public class GetOrderNumber
    {
        public static int NextOrderNumber(DateTime orderDate)
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            FileRepository prodRepo = new FileRepository();
            TestRepository testRepo = new TestRepository();

            switch (mode)
            {
                case "Test":
                    return testRepo.LoadOrders(orderDate).Count()+1;
                case "Prod":
                    return prodRepo.LoadOrders(orderDate).Count()+1;
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }

        }
    }
}
