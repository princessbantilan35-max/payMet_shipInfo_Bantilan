using OrderModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ShippingDataService
{
    public class OrderJsonData
    {
        private List<OrderModel> orders = new List<OrderModel>();

        private string _jsonFileName;

        public OrderJsonData()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/ShippingInfos.json";
            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            if (!File.Exists(_jsonFileName))
            {
                using (File.Create(_jsonFileName)) { }
            }

            RetrieveDataFromJsonFile();

            if (orders.Count <= 0)
            {
                orders.Add(new OrderModel
                {
                    OrderID = "SHIP-DEFAULT-1",
                    CustomerName = "Princess Bantilan",
                    PhoneNumber = "09123456789",
                    ShippingAddress = "Biñan City, Laguna",
                    ItemName = "Sample Item",
                    Quantity = 1,
                    Price = 100,
                    ShippingMethod = "Standard",
                    OrderStatus = "Pending",
                    OrderDate = DateTime.Now,
                    EstimatedDelivery = DateTime.Now.AddDays(5)
                });

                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            using (var outputStream = File.Open(_jsonFileName, FileMode.Create))
            {
                JsonSerializer.Serialize(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    orders
                );
            }
        }

        private void RetrieveDataFromJsonFile()
        {
            if (!File.Exists(_jsonFileName))
            {
                orders = new List<OrderModel>();
                return;
            }

            var json = File.ReadAllText(_jsonFileName);

            if (string.IsNullOrWhiteSpace(json))
            {
                orders = new List<OrderModel>();
                return;
            }

            try
            {
                orders = JsonSerializer.Deserialize<List<OrderModel>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new List<OrderModel>();
            }
            catch
            {
                orders = new List<OrderModel>();
            }
        }

        public void Add(OrderModel order)
        {
            RetrieveDataFromJsonFile();
            orders.Add(order);
            SaveDataToJsonFile();
        }

        public List<OrderModel> GetOrders()
        {
            RetrieveDataFromJsonFile();
            return orders;
        }

        public OrderModel? GetById(string id)
        {
            RetrieveDataFromJsonFile();
            return orders.FirstOrDefault(x => x.OrderID == id);
        }

        public void Update(OrderModel updatedOrder)
        {
            RetrieveDataFromJsonFile();

            var index = orders.FindIndex(x => x.OrderID == updatedOrder.OrderID);

            if (index != -1)
            {
                orders[index] = updatedOrder;
            }

            SaveDataToJsonFile();
        }

        public void Delete(string id)
        {
            RetrieveDataFromJsonFile();

            var order = orders.FirstOrDefault(x => x.OrderID == id);

            if (order != null)
            {
                orders.Remove(order);
            }

            SaveDataToJsonFile();
        }
    }
}