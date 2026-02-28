using System;
using System.Collections.Generic;

namespace ShippingandPayment_Management_System
{
    internal class Program
    {
        static List<string> orderIDs = new List<string>();
        static List<string> customerNames = new List<string>();
        static List<string> shippingAddresses = new List<string>();
        static List<string> itemNames = new List<string>();
        static List<int> quantities = new List<int>();
        static List<double> prices = new List<double>();
        static List<string> shippingMethods = new List<string>();
        static List<string> orderStatuses = new List<string>();
        static List<DateTime> orderDates = new List<DateTime>();
        static List<DateTime> estimatedDeliveries = new List<DateTime>();

        static void Main(string[] args)
        {
            Console.WriteLine("----- SHIPPING MANAGEMENT SYSTEM -----");
            AdminMenu();
        }

        static void AdminMenu()
        {
            Console.WriteLine("\nMAIN MENU:\n");

            string[] options = new string[]
            {
                "Create Order",
                "View Orders",
                "Delete Order",
                "Exit"
            };

            ShowOptions(options);
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
                    Console.WriteLine("Invalid option.");
                    AdminMenu();
                    break;
            }
        }

        static void CreateOrder()
        {
            Console.WriteLine("\nCREATE ORDER:");

            string orderID = "ORD" + (orderIDs.Count + 1);
            DateTime orderDate = DateTime.Now;

            Console.Write("Customer Name: ");
            string customerName = Console.ReadLine();

            Console.Write("Shipping Address: ");
            string shippingAddress = Console.ReadLine();

            Console.Write("Item Name: ");
            string itemName = Console.ReadLine();

            Console.Write("Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Console.Write("Price per Item: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Shipping Method (Standard/Express): ");
            string shippingMethod = Console.ReadLine();

            DateTime estimatedDelivery;

            if (shippingMethod.ToLower() == "express")
                estimatedDelivery = orderDate.AddDays(2);
            else
                estimatedDelivery = orderDate.AddDays(5);

            string orderStatus = "Processing";

            orderIDs.Add(orderID);
            customerNames.Add(customerName);
            shippingAddresses.Add(shippingAddress);
            itemNames.Add(itemName);
            quantities.Add(quantity);
            prices.Add(price);
            shippingMethods.Add(shippingMethod);
            orderStatuses.Add(orderStatus);
            orderDates.Add(orderDate);
            estimatedDeliveries.Add(estimatedDelivery);

            Console.WriteLine("Order Created Successfully!");
            AdminMenu();
        }

        static void ViewOrders()
        {
            if (orderIDs.Count == 0)
            {
                Console.WriteLine("No orders available.");
                AdminMenu();
                return;
            }

            for (int i = 0; i < orderIDs.Count; i++)
            {
                double subtotal = quantities[i] * prices[i];

                double shippingFee;

                if (shippingMethods[i].ToLower() == "express")
                    shippingFee = 150;
                else
                    shippingFee = 80;

                double total = subtotal + shippingFee;

                Console.WriteLine("\n--------------------------------------------------");
                Console.WriteLine("ORDER DETAILS");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Order ID: " + orderIDs[i]);
                Console.WriteLine("Order Date: " + orderDates[i]);
                Console.WriteLine("Estimated Delivery: " + estimatedDeliveries[i]);
                Console.WriteLine("Order Status: " + orderStatuses[i]);
                Console.WriteLine("\nCustomer: " + customerNames[i]);
                Console.WriteLine("Shipping Address: " + shippingAddresses[i]);
                Console.WriteLine("\nItem: " + itemNames[i]);
                Console.WriteLine("Quantity: " + quantities[i]);
                Console.WriteLine("Subtotal: " + subtotal + " pesos");
                Console.WriteLine("Shipping Fee: " + shippingFee + " pesos");
                Console.WriteLine("Total: " + total + " pesos");
                Console.WriteLine("--------------------------------------------------");
            }

            AdminMenu();
        }

        static void DeleteOrder()
        {
            Console.Write("Enter Order ID to delete: ");
            string findID = Console.ReadLine();

            for (int i = 0; i < orderIDs.Count; i++)
            {
                if (orderIDs[i] == findID)
                {
                    orderIDs.RemoveAt(i);
                    customerNames.RemoveAt(i);
                    shippingAddresses.RemoveAt(i);
                    itemNames.RemoveAt(i);
                    quantities.RemoveAt(i);
                    prices.RemoveAt(i);
                    shippingMethods.RemoveAt(i);
                    orderStatuses.RemoveAt(i);
                    orderDates.RemoveAt(i);
                    estimatedDeliveries.RemoveAt(i);

                    Console.WriteLine("Order Deleted Successfully!");
                    break;
                }
            }

            AdminMenu();
        }

        static void ShowOptions(string[] options)
        {
            for (int x = 0; x < options.Length; x++)
            {
                Console.WriteLine($"[{x + 1}] {options[x]}");
            }

            Console.Write("Enter the number of your option: ");
        }
    }
}