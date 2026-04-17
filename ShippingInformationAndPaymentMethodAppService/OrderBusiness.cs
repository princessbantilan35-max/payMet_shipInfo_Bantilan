using OrderModels;
using ShippingDataService;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class OrderBusiness
    {
        OrderDBData dataService = new OrderDBData();

        public List<OrderModel> GetShippingInformations()
        {
            return dataService.GetOrders();
        }

        public void CreateShippingInformation(OrderModel order)
        {
            dataService.Add(order);
        }

        public void UpdateShippingInformation(OrderModel order)
        {
            dataService.Update(order);
        }

        public void DeleteShippingInformation(string orderId)
        {
            dataService.Delete(orderId);
        }

        public bool ProcessPayment(OrderModel order, double amount, string method)
        {
            if (amount >= order.Total())
            {
                order.OrderStatus = "Paid";
                dataService.Update(order);
                return true;
            }
            return false;
        }

        public bool IsValidShippingInformation(OrderModel order)
        {
            return !string.IsNullOrEmpty(order.CustomerName) &&
                   !string.IsNullOrEmpty(order.PhoneNumber) &&
                   !string.IsNullOrEmpty(order.ShippingAddress) &&
                   order.Quantity > 0 &&
                   order.Price > 0;
        }
    }
}