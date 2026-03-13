using System;
using System.Collections.Generic;
using BusinessLogic;
using OrderModels;

namespace ShippingandPayment_Management_System
{
    public class Program
    {
        static OrderBusiness orderBusiness = new OrderBusiness();

        public static void Main(string[] args)
        {
            Console.WriteLine("----- SHIPPING MANAGEMENT SYSTEM -----");
            AdminMenu();
        }

        public static void AdminMenu()
        {
            Console.WriteLine("\nMAIN MENU\n");

            Console.WriteLine("[1] Create Order");
            Console.WriteLine("[2] View Orders");
            Console.WriteLine("[3] Delete Order");
            Console.WriteLine("[4] Exit");

            Console.Write("Enter option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateOrder();
                    break;
                case "2":
                    ViewOrders();
                    break;
                case "3":
                    DeleteOrder();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    AdminMenu();
                    break;
            }
        }

        public static void CreateOrder()
        {
            OrderModel order = new OrderModel();

            order.OrderID = "ORD" + DateTime.Now.Ticks;
            order.OrderDate = DateTime.Now;

            Console.Write("Customer Name: ");
            order.CustomerName = Console.ReadLine();

            Console.Write("Phone Number: ");
            order.PhoneNumber = Console.ReadLine();

            Console.Write("Shipping Address: ");
            order.ShippingAddress = Console.ReadLine();

            Console.Write("Item Name: ");
            order.ItemName = Console.ReadLine();

            Console.Write("Quantity: ");
            order.Quantity = Convert.ToInt32(Console.ReadLine());

            Console.Write("Price per Item: ");
            order.Price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Shipping Method (Standard/Express): ");
            order.ShippingMethod = Console.ReadLine();

            order.OrderStatus = "Processing";

            if (order.ShippingMethod.ToLower() == "express")
                order.EstimatedDelivery = order.OrderDate.AddDays(2);
            else
                order.EstimatedDelivery = order.OrderDate.AddDays(5);

            orderBusiness.CreateOrder(order);

            Console.WriteLine("Order created successfully!");
            AdminMenu();
        }

        public static void ViewOrders()
        {
            var orders = orderBusiness.GetOrders();

            if (orders.Count == 0)
            {
                Console.WriteLine("No orders available.");
            }

            for (int i = 0; i < orders.Count; i++)
            {
                var o = orders[i];

                Console.WriteLine("\n--------------------------------");
                Console.WriteLine("Order ID: " + o.OrderID);
                Console.WriteLine("Customer: " + o.CustomerName);
                Console.WriteLine("Item: " + o.ItemName);
                Console.WriteLine("Quantity: " + o.Quantity);
                Console.WriteLine("Total: " + o.Total() + " pesos");
                Console.WriteLine("--------------------------------");
            }

            AdminMenu();
        }

        public static void DeleteOrder()
        {
            Console.Write("Enter Order ID: ");
            string id = Console.ReadLine();

            orderBusiness.DeleteOrder(id);

            Console.WriteLine("Order deleted.");
            AdminMenu();
        }
    }
}