using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> LoadOrders(DateTime OrderDate);
        Order SaveOrders(Order order);
        Order LocateOrder(DateTime OrderDate, int OrderNum);
        List<Order> RemoveOrder(Order order);
        List<Order> UpdateOrder(Order order);
    }
}
