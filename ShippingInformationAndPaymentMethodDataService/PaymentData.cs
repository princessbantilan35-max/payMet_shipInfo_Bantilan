using PayMethodShipInfoAPI.Models;

public static class PaymentData
{
    public static List<PaymentModel> Payments = new List<PaymentModel>();

    public static void Add(PaymentModel payment) => Payments.Add(payment);

    public static List<PaymentModel> GetAll() => new List<PaymentModel>(Payments);

    public static PaymentModel GetById(string paymentId) =>
        Payments.FirstOrDefault(p => p.PaymentID == paymentId);

    public static void Update(PaymentModel payment)
    {
        int index = Payments.FindIndex(p => p.PaymentID == payment.PaymentID);
        if (index >= 0) Payments[index] = payment;
    }

    public static void Delete(string paymentId)
    {
        var payment = GetById(paymentId);
        if (payment != null) Payments.Remove(payment);
    }
}