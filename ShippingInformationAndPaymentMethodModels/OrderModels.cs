using System;
using System.Collections.Generic;
using System.Text;

namespace OrderModels
{
        public class OrderModel
        {
            public string OrderID;
            public string CustomerName;
            public string PhoneNumber;
            public string ShippingAddress;
            public string ItemName;
            public int Quantity;
            public double Price;
            public string ShippingMethod;
            public string OrderStatus;
            public System.DateTime OrderDate;
            public System.DateTime EstimatedDelivery;

            // Calculate total (subtotal + shipping fee)
            public double Total()
            {
                double subtotal = Quantity * Price;
                double shippingFee = (ShippingMethod.ToLower() == "express") ? 150 : 80;
                return subtotal + shippingFee;
            }
        }
    }