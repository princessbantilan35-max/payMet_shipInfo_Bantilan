using System;

namespace OrderModels
{
    public class OrderModel
    {
        public string OrderID { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ShippingMethod { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime EstimatedDelivery { get; set; }
        public double Total()
        {
            models: double subtotal = Quantity * Price;
            double shippingFee = (ShippingMethod?.ToLower() == "express") ? 150 : 80;
            return subtotal + shippingFee;

        }
    }
}