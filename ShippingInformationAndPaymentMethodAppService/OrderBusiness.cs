using OrderModels;
using ShippingDataService;
using PayMethodShipInfoAPI.Models;

namespace BusinessLogic
{
    public class OrderBusiness
    {
        private readonly OrderJsonData dataService = new OrderJsonData();

       
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

        public bool IsValidShippingInformation(OrderModel order)
        {
            return !string.IsNullOrEmpty(order.CustomerName) &&
                   !string.IsNullOrEmpty(order.PhoneNumber) &&
                   !string.IsNullOrEmpty(order.ShippingAddress) &&
                   order.Quantity > 0 &&
                   order.Price > 0;
        }

        public List<PaymentModel> GetPayments()
        {
            return PaymentData.GetAll();
        }

        public PaymentModel? GetPaymentById(string paymentId)
        {
            return PaymentData.GetById(paymentId);
        }

        public PaymentModel? GetPaymentByOrderId(string orderId)
        {
            return PaymentData.GetAll()
                .Find(p => p.OrderID == orderId);
        }

        public bool CreatePayment(PaymentModel payment)
        {
            if (payment == null)
            {
                return false;
            }

            PaymentData.Add(payment);
            return true;
        }

        public void UpdatePayment(PaymentModel payment)
        {
            PaymentData.Update(payment);
        }

        public void DeletePayment(string paymentId)
        {
            PaymentData.Delete(paymentId);
        }
    }
}