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
            Console.WriteLine("----- SHIPPING & PAYMENT MANAGEMENT SYSTEM -----");
            MainMenu();
        }

        public static void MainMenu()
        {
            Console.WriteLine("\nMAIN MENU\n");
            Console.WriteLine("[1] Create Shipping Information");
            Console.WriteLine("[2] View Shipping Information");
            Console.WriteLine("[3] Update Shipping Information");
            Console.WriteLine("[4] Delete Shipping Information");
            Console.WriteLine("[5] Process Payment");
            Console.WriteLine("[6] Exit Program");

            Console.Write("Enter option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateShippingInformation();
                    break;
                case "2":
                    ViewShippingInformation();
                    break;
                case "3":
                    UpdateShippingInformation();
                    break;
                case "4":
                    DeleteShippingInformation();
                    break;
                case "5":
                    ProcessPayment();
                    break;
                case "6":
                    Console.WriteLine("Thank you for using Shipping & Payment Management System!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    MainMenu();
                    break;
            }
        }

        public static void CreateShippingInformation()
        {
            OrderModel shippingInfo = new OrderModel();

            shippingInfo.OrderID = "SHIP" + DateTime.Now.Ticks;
            shippingInfo.OrderDate = DateTime.Now;

            Console.Write("Customer Name: ");
            shippingInfo.CustomerName = Console.ReadLine();

            Console.Write("Phone Number: ");
            shippingInfo.PhoneNumber = Console.ReadLine();

            Console.Write("Shipping Address: ");
            shippingInfo.ShippingAddress = Console.ReadLine();

            Console.Write("Item Name: ");
            shippingInfo.ItemName = Console.ReadLine();

            Console.Write("Quantity: ");
            shippingInfo.Quantity = Convert.ToInt32(Console.ReadLine());

            Console.Write("Price per Item: ");
            shippingInfo.Price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Shipping Method (Standard/Express): ");
            shippingInfo.ShippingMethod = Console.ReadLine();

            shippingInfo.OrderStatus = "Pending";

            if (!string.IsNullOrEmpty(shippingInfo.ShippingMethod) &&
                shippingInfo.ShippingMethod.ToLower() == "express")
                shippingInfo.EstimatedDelivery = shippingInfo.OrderDate.AddDays(2);
            else
                shippingInfo.EstimatedDelivery = shippingInfo.OrderDate.AddDays(5);

            if (orderBusiness.IsValidShippingInformation(shippingInfo))
            {
                orderBusiness.CreateShippingInformation(shippingInfo);
                Console.WriteLine($"\n✅ Shipping information created successfully!");
                Console.WriteLine($"Shipping ID: {shippingInfo.OrderID}");
                Console.WriteLine($"Estimated Delivery: {shippingInfo.EstimatedDelivery:MM/dd/yyyy}");
                Console.WriteLine($"Total Amount: {shippingInfo.Total():F2} pesos");
            }
            else
            {
                Console.WriteLine("\n❌ Validation failed! Please check your input.");
            }

            MainMenu();
        }

        public static void ViewShippingInformation()
        {
            var shippingInfos = orderBusiness.GetShippingInformations();

            if (shippingInfos.Count == 0)
            {
                Console.WriteLine("No shipping information available.");
                MainMenu();
                return;
            }

            Console.WriteLine("\n----- SHIPPING INFORMATION -----");
            for (int i = 0; i < shippingInfos.Count; i++)
            {
                var info = shippingInfos[i];
                Console.WriteLine($"\n[{i + 1}] Shipping ID: {info.OrderID}");
                Console.WriteLine($"    Customer: {info.CustomerName}");
                Console.WriteLine($"    Phone: {info.PhoneNumber}");
                Console.WriteLine($"    Address: {info.ShippingAddress}");
                Console.WriteLine($"    Item: {info.ItemName} (Qty: {info.Quantity})");
                Console.WriteLine($"    Shipping: {info.ShippingMethod}");
                Console.WriteLine($"    Status: {info.OrderStatus}");
                Console.WriteLine($"    Est. Delivery: {info.EstimatedDelivery:MM/dd/yyyy}");
                Console.WriteLine($"    Total: {info.Total():F2} pesos");
                Console.WriteLine("    " + new string('-', 50));
            }
            MainMenu();
        }

        public static void UpdateShippingInformation()
        {
            var shippingInfos = orderBusiness.GetShippingInformations();

            if (shippingInfos.Count == 0)
            {
                Console.WriteLine("No shipping information available to update!");
                MainMenu();
                return;
            }

            Console.WriteLine("\n===== SELECT SHIPPING TO UPDATE =====");
            for (int i = 0; i < shippingInfos.Count; i++)
            {
                var info = shippingInfos[i];
                Console.WriteLine($"\n[{i + 1}] Shipping ID: {info.OrderID}");
                Console.WriteLine($"    Customer: {info.CustomerName}");
                Console.WriteLine($"    Phone: {info.PhoneNumber}");
                Console.WriteLine($"    Address: {info.ShippingAddress}");
                Console.WriteLine($"    Status: {info.OrderStatus}");
                Console.WriteLine("    " + new string('-', 40));
            }

            Console.Write("\nEnter number to update (1-" + shippingInfos.Count + "): ");
            string choiceStr = Console.ReadLine();

            if (!int.TryParse(choiceStr, out int choice) || choice < 1 || choice > shippingInfos.Count)
            {
                Console.WriteLine("Invalid selection!");
                MainMenu();
                return;
            }

            var shippingInfo = shippingInfos[choice - 1];
            Console.WriteLine($"\n✅ Selected: {shippingInfo.OrderID} - {shippingInfo.CustomerName}");

            Console.WriteLine("\n===== CURRENT INFORMATION =====");
            Console.WriteLine($"Shipping ID: {shippingInfo.OrderID}");
            Console.WriteLine($"1. Customer Name: {shippingInfo.CustomerName}");
            Console.WriteLine($"2. Phone Number: {shippingInfo.PhoneNumber}");
            Console.WriteLine($"3. Shipping Address: {shippingInfo.ShippingAddress}");
            Console.WriteLine($"4. Shipping Method: {shippingInfo.ShippingMethod}");
            Console.WriteLine($"5. Order Status: {shippingInfo.OrderStatus}");
            Console.WriteLine($"   Est. Delivery: {shippingInfo.EstimatedDelivery:MM/dd/yyyy}");

            Console.WriteLine("\nWhat do you want to update?");
            Console.WriteLine("[1] Customer Name");
            Console.WriteLine("[2] Phone Number");
            Console.WriteLine("[3] Shipping Address");
            Console.WriteLine("[4] Shipping Method");
            Console.WriteLine("[5] Order Status");
            Console.WriteLine("[6] Cancel");

            Console.Write("Enter choice: ");
            string fieldChoice = Console.ReadLine();

            string input;

            switch (fieldChoice)
            {
                case "1":
                    Console.Write("Enter new Customer Name: ");
                    input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                        shippingInfo.CustomerName = input;
                    break;

                case "2":
                    Console.Write("Enter new Phone Number: ");
                    input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                        shippingInfo.PhoneNumber = input;
                    break;

                case "3":
                    Console.Write("Enter new Shipping Address: ");
                    input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                        shippingInfo.ShippingAddress = input;
                    break;

                case "4":
                    Console.Write("Enter Shipping Method (Standard/Express): ");
                    input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                    {
                        shippingInfo.ShippingMethod = input;
                        if (shippingInfo.ShippingMethod.ToLower() == "express")
                            shippingInfo.EstimatedDelivery = shippingInfo.OrderDate.AddDays(2);
                        else
                            shippingInfo.EstimatedDelivery = shippingInfo.OrderDate.AddDays(5);
                    }
                    break;

                case "5":
                    Console.Write("Enter new Order Status: ");
                    input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                        shippingInfo.OrderStatus = input;
                    break;

                case "6":
                    Console.WriteLine("Update cancelled.");
                    MainMenu();
                    return;

                default:
                    Console.WriteLine("Invalid choice!");
                    MainMenu();
                    return;
            }

            if (orderBusiness.IsValidShippingInformation(shippingInfo))
            {
                orderBusiness.UpdateShippingInformation(shippingInfo);
                Console.WriteLine($"\n✅ Shipping information updated successfully!");
                Console.WriteLine($"Updated Shipping ID: {shippingInfo.OrderID}");
            }
            else
            {
                Console.WriteLine("\n❌ Validation failed! Please check your input.");
            }

            MainMenu();
        }

        public static void DeleteShippingInformation()
        {
            var shippingInfos = orderBusiness.GetShippingInformations();

            if (shippingInfos.Count == 0)
            {
                Console.WriteLine("No shipping information available to delete!");
                MainMenu();
                return;
            }

            Console.WriteLine("\n===== SELECT SHIPPING TO DELETE =====");
            for (int i = 0; i < shippingInfos.Count; i++)
            {
                var info = shippingInfos[i];
                Console.WriteLine($"\n[{i + 1}] Shipping ID: {info.OrderID}");
                Console.WriteLine($"    Customer: {info.CustomerName}");
                Console.WriteLine($"    Phone: {info.PhoneNumber}");
                Console.WriteLine($"    Status: {info.OrderStatus}");
                Console.WriteLine("    " + new string('-', 40));
            }

            Console.Write("\nEnter number to delete (1-" + shippingInfos.Count + "): ");
            string choiceStr = Console.ReadLine();

            if (!int.TryParse(choiceStr, out int choice) || choice < 1 || choice > shippingInfos.Count)
            {
                Console.WriteLine("Invalid selection!");
                MainMenu();
                return;
            }

            var shippingInfo = shippingInfos[choice - 1];
            Console.WriteLine($"\n⚠️  Confirm deletion:");
            Console.WriteLine($"Shipping ID: {shippingInfo.OrderID}");
            Console.WriteLine($"Customer: {shippingInfo.CustomerName}");
            Console.WriteLine($"Item: {shippingInfo.ItemName}");
            Console.WriteLine($"Total: {shippingInfo.Total():F2} pesos");

            Console.Write("Are you sure? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                orderBusiness.DeleteShippingInformation(shippingInfo.OrderID);
                Console.WriteLine("\n✅ Shipping information deleted successfully!");
            }
            else
            {
                Console.WriteLine("\n❌ Deletion cancelled.");
            }

            MainMenu();
        }

        public static void ProcessPayment()
        {
            var shippingInfos = orderBusiness.GetShippingInformations();

            if (shippingInfos.Count == 0)
            {
                Console.WriteLine("No shipping information available for payment!");
                MainMenu();
                return;
            }

            Console.WriteLine("\n===== SELECT SHIPPING FOR PAYMENT =====");
            for (int i = 0; i < shippingInfos.Count; i++)
            {
                var info = shippingInfos[i];
                Console.WriteLine($"\n[{i + 1}] Shipping ID: {info.OrderID}");
                Console.WriteLine($"    Customer: {info.CustomerName}");
                Console.WriteLine($"    Item: {info.ItemName}");
                Console.WriteLine($"    Total: {info.Total():F2} pesos");
                Console.WriteLine($"    Status: {info.OrderStatus}");
                Console.WriteLine("    " + new string('-', 40));
            }

            Console.Write("\nEnter number for payment (1-" + shippingInfos.Count + "): ");
            string choiceStr = Console.ReadLine();

            if (!int.TryParse(choiceStr, out int choice) || choice < 1 || choice > shippingInfos.Count)
            {
                Console.WriteLine("Invalid selection!");
                MainMenu();
                return;
            }

            var order = shippingInfos[choice - 1];
            Console.WriteLine($"\n✅ Selected: {order.OrderID} - {order.CustomerName}");

            Console.WriteLine($"\n----- PAYMENT PROCESSING -----");
            Console.WriteLine($"Customer: {order.CustomerName}");
            Console.WriteLine($"Item: {order.ItemName}");
            Console.WriteLine($"Quantity: {order.Quantity}");
            Console.WriteLine($"Total Amount: {order.Total():F2} pesos");
            Console.WriteLine($"Current Status: {order.OrderStatus}");

            Console.Write("Payment Method (Cash/Card): ");
            string paymentMethod = Console.ReadLine();

            Console.Write("Enter payment amount: ");
            if (!double.TryParse(Console.ReadLine(), out double paymentAmount) || paymentAmount < 0)
            {
                Console.WriteLine("❌ Invalid payment amount!");
                MainMenu();
                return;
            }

            if (paymentAmount >= order.Total())
            {
                bool paymentSuccess = orderBusiness.ProcessPayment(order, paymentAmount, paymentMethod);

                if (paymentSuccess)
                {
                    double change = paymentAmount - order.Total();
                    Console.WriteLine($"\n✅ Payment processed successfully!");
                    Console.WriteLine($"Payment Method: {paymentMethod}");
                    Console.WriteLine($"Amount Paid: {paymentAmount:F2} pesos");
                    if (change > 0)
                        Console.WriteLine($"Change: {change:F2} pesos");
                    Console.WriteLine($"New Status: {order.OrderStatus}");
                }
                else
                {
                    Console.WriteLine("❌ Payment processing failed!");
                }
            }
            else
            {
                Console.WriteLine("❌ Insufficient payment amount!");
            }
            MainMenu();
        }
    }

}