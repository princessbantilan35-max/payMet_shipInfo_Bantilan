using System;
using System.Collections.Generic;
using System.Text;
using OrderModels;

namespace ShippingInformationAndPaymentMethodDataService
{
    public static class OrderData
    {
        public static List<OrderModel> Orders = new List<OrderModel>();

        public static void AddOrder(OrderModel order)
        {
            Orders.Add(order);
        }

        public static void RemoveOrder(string orderID)
        {
            Orders.RemoveAll(o => o.OrderID == orderID);
        }

        public static List<OrderModel> GetOrders()
        {
            return Orders;
        }
    }
}
