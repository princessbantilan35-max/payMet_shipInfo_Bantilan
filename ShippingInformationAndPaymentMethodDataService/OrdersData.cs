using System;
using System.Collections.Generic;
using System.Linq;
using OrderModels;

namespace ShippingInformationAndPaymentMethodDataService
{
    public static class OrderData
    {
        public static List<OrderModel> ShippingInformations = new List<OrderModel>();

        public static void CreateShippingInformation(OrderModel shippingInfo)
        {
            ShippingInformations.Add(shippingInfo);
        }

        public static void DeleteShippingInformation(string shippingId)
        {
            var shippingInfo = GetShippingInformationById(shippingId);
            if (shippingInfo != null)
            {
                ShippingInformations.Remove(shippingInfo);
            }
        }

        public static List<OrderModel> GetShippingInformations()
        {
            return new List<OrderModel>(ShippingInformations);
        }

        public static OrderModel GetShippingInformationById(string shippingId)
        {
            return ShippingInformations.FirstOrDefault(o => o.OrderID == shippingId);
        }

        public static void UpdateShippingInformation(OrderModel shippingInfo)
        {
            int index = ShippingInformations.FindIndex(o => o.OrderID == shippingInfo.OrderID);
            if (index >= 0)
            {
                ShippingInformations[index] = shippingInfo; 
            }
        }
        public static void AddOrder(OrderModel order) => CreateShippingInformation(order);
        public static void RemoveOrder(string orderId) => DeleteShippingInformation(orderId);
        public static List<OrderModel> GetOrders() => GetShippingInformations();
    }
}