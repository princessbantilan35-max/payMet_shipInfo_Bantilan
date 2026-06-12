using System;

namespace PayMethodShipInfoAPI.Models
{
    public class PaymentModel
    {
        public string? PaymentID { get; set; }
        public string? OrderID { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Details { get; set; }
        public double Amount { get; set; }
        public string? Status { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}