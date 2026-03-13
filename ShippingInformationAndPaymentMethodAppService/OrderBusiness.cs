using System;
using System.Collections.Generic;
using System.Linq;
using OrderModels;
using ShippingInformationAndPaymentMethodDataService;

namespace BusinessLogic
{
    public class OrderBusiness
    {
        List<OrderModel> orders = new List<OrderModel>();

        public void CreateOrder(OrderModel order)
        {
            orders.Add(order);
        }

        public List<OrderModel> GetOrders()
        {
            return orders.OrderByDescending(o => o.OrderDate).ToList();
        }

        public void DeleteOrder(string orderID)
        {
            var orderToRemove = orders.FirstOrDefault(o => o.OrderID == orderID);
            if (orderToRemove != null)
            {
                orders.Remove(orderToRemove);
                Console.WriteLine($"Order {orderID} deleted successfully!");
            }
            else
            {
                Console.WriteLine($"Order {orderID} not found!");
            }
        }

        public void DisplayOrders(string filterCustomer = null)
        {
            var filteredOrders = string.IsNullOrEmpty(filterCustomer)
                ? orders
                : orders.Where(o => o.CustomerName.ToLower().Contains(filterCustomer.ToLower())).ToList();

            if (!filteredOrders.Any())
            {
                Console.WriteLine("No orders found.");
                return;
            }
            foreach (var o in filteredOrders)
            {
                Console.WriteLine("\n--------------------------------------------------");
                Console.WriteLine("Order ID: " + o.OrderID);
                Console.WriteLine("Customer: " + o.CustomerName);
                Console.WriteLine("Phone: " + o.PhoneNumber);
                Console.WriteLine("Shipping Address: " + o.ShippingAddress);
                Console.WriteLine("Item: " + o.ItemName);
                Console.WriteLine("Quantity: " + o.Quantity);
                Console.WriteLine("Price per Item: " + o.Price);
                Console.WriteLine("Shipping Method: " + o.ShippingMethod);
                Console.WriteLine("Order Status: " + o.OrderStatus);
                Console.WriteLine("Order Date: " + o.OrderDate);
                Console.WriteLine("Estimated Delivery: " + o.EstimatedDelivery);
                Console.WriteLine("Total: " + o.Total() + " pesos");
                Console.WriteLine("--------------------------------------------------");
            }
        }
        public List<OrderModel> GetHighValueOrders(double minTotal)
        {
            return orders.Where(o => o.Total() >= minTotal)
                         .OrderByDescending(o => o.Total())
                         .ToList();
        }
    }
}