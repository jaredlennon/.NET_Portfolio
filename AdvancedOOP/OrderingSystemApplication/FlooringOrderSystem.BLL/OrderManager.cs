using FlooringOrderSystem.Models;
using FlooringOrderSystem.Models.Interfaces;
using FlooringOrderSystem.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrderSystem.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public DisplayOrdersResponse DisplayOrders(DateTime orderDate)
        {
            DisplayOrdersResponse response = new DisplayOrdersResponse();

            response.Orders = _orderRepository.LoadOrders(orderDate);

            if(response.Orders.Count == 0)
            {
                response.Success = false;
                response.Message = $"No orders found for {orderDate:MM/dd/yyyy}.";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public AddOrderResponse AddOrder(Order order)
        {   
            order = GetCalculatedFieldsFromFile.CalculateFieldsFromFile(order);

            AddOrderResponse response = new AddOrderResponse();

            response.Order = _orderRepository.SaveOrders(order);

            if(response.Order.CustomerName == null)
            {
                response.Success = false;
                response.Message = $"Unable to add order.";
            }
            else
            {
                response.Success = true;
            }
            return response;

        }

        public LocateOrderResponse LocateOrder(DateTime orderDate, int orderNum)
        {            
            LocateOrderResponse response = new LocateOrderResponse();

            response.Order = _orderRepository.LocateOrder(orderDate, orderNum);

            if(response.Order == null)
            {
                response.Success = false;
                response.Message = $"Unable to locate order #{orderNum} on {orderDate:MM/dd/yyyy}.";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public RemoveOrderResponse RemoveOrder(Order order)
        {
            RemoveOrderResponse removeResponse = new RemoveOrderResponse();

            removeResponse.Order = _orderRepository.RemoveOrder(order);

            if(removeResponse.Order == null)
            {
                removeResponse.Success = false;
                removeResponse.Message = $"No orders found for {order.OrderDate:MM/dd/yyyy}.";
            }
            else
            {
                removeResponse.Success = true;
            }

            return removeResponse;

        }

        public UpdateOrderResponse UpdateOrder(Order order)
        {   
            order = GetCalculatedFieldsFromFile.CalculateFieldsFromFile(order);

            UpdateOrderResponse response = new UpdateOrderResponse();

            response.Orders = _orderRepository.UpdateOrder(order);

            if (response.Orders.Count == 0)
            {
                response.Success = false;
                response.Message = $"No orders found for {order.OrderDate:MM/dd/yyyy}.";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

    }
}
